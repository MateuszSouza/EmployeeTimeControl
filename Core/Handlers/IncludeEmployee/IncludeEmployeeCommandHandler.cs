using Domain.Model;
using MediatR;
using Repository.IRepository;
using BCrypt;

namespace Core.Handlers.IncludeEmployee
{
    public class IncludeEmployeeCommandHandler : IRequestHandler<IncludeEmployeeCommandRequest, IncludeEmployeeCommandResponse>
    {
        private readonly IEmployeeRepository EmployeeRepository;

        public IncludeEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            EmployeeRepository = employeeRepository;
        }

        public async Task<IncludeEmployeeCommandResponse> Handle(IncludeEmployeeCommandRequest request, CancellationToken cancellationToken)
        {
            IncludeEmployeeCommandResponse response;
            try
            {
                var employee = await EmployeeRepository.GetEmployee(request.Email);
                if (employee == null)
                {
                    var newEmployee = new Employee
                    {
                        Email = request.Email,
                        EmployeeName = request.EmployeeName,
                        Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                        Roles = request.Roles
                    };
                    await EmployeeRepository.InsertEmployee(newEmployee);

                    response = new IncludeEmployeeCommandResponse
                    {
                        message = "Employee Included successfuly."
                    };
                }
                else
                {
                    response = new IncludeEmployeeCommandResponse
                            {
                                message = "Employee alread exists."
                            };
                }
            }
            catch(Exception ex)
            {
                response = new IncludeEmployeeCommandResponse
                {     
                    message = "an unexpected error occurred"
                };

            }
            return (response);
            
        }
    }
}