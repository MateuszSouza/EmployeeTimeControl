using MediatR;

namespace Core.Handlers.IncludeEmployee
{
    public class IncludeEmployeeCommandRequest : IRequest<IncludeEmployeeCommandResponse>
    {
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
    }
}