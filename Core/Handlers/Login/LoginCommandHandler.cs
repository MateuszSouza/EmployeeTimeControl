using Domain.Model;
using Domain.Token;
using MediatR;
using Repository.IRepository;

namespace Core.Handlers.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, LoginCommandResponse>
    {
        private TokenService TokenService { get; set; }
        private IEmployeeRepository EmployeeRepository { get; set; }

        public LoginCommandHandler(TokenService tokenService, IEmployeeRepository employeeRepository)
        {
            TokenService = tokenService;
            EmployeeRepository = employeeRepository;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            var Employee = await EmployeeRepository.GetEmployee(request.Email);

            LoginCommandResponse retorno;

            if (Employee == null)
            {
                Employee.EmployeeName = "";
                Employee.Email = "";
                Employee.Roles = "";
                
                retorno = new LoginCommandResponse()
                {
                    message =  "Employee not found"
                };
                return retorno;
            }

             retorno = new LoginCommandResponse()
            {
                Token = TokenService.GeradorDeToken(Employee),
                Role = "funcionario",
                message = "Success"
            };

            return retorno;
        }
    }
}