using Dapper;
using Domain.Model;
using Microsoft.Extensions.Configuration;
using Repository.IRepository;

namespace Repository.Repositories
{
    public class EmployeeRepository : DatabaseConnection, IEmployeeRepository
    {
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        { }

        public void DeleteEmployee(Employee employee)
        {
            var connection = GetOpendConnections();

            var command = @"DELETE FROM employee WHERE id = @id";

            connection.ExecuteAsync(command, employee.Id);

            CloseConnection();
        }

        public async Task<Employee> GetEmployee(string email)
        {
            var connection = GetOpendConnections();

            var command = @"SELECT
                            id AS Id
                            ,employee_name AS EmployeeName
                            ,email AS Email
                            ,_password AS Password
                            ,_role AS Roles
                        FROM employee WHERE email = @Email";

            Employee employee;

            try
            {
                employee = await connection.QueryFirstOrDefaultAsync<Employee>(command, new
                {
                    Email = email
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            CloseConnection();

            return employee;
        }

        public Employee GetEmployeeByEmailAndPassword(string email, string password)
        {
            throw new NotImplementedException();
        }

        public async Task InsertEmployee(Employee employee)
        {
            var connection = GetOpendConnections();

            var command = @"INSERT INTO Employee
                         (
                             Employee_Name, email, _password, _role
                         )
                             VALUES
                         (
                            @NomeEmploye, @Email, @Password, @Role
                         )";
            await connection.ExecuteAsync(command, new
            {
                NomeEmploye = employee.EmployeeName,
                Email = employee.Email,
                Password = employee.Password,
                Role = employee.Roles,
            });

            CloseConnection();
        }

        public void UpdateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}