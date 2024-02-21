using Domain.Model;

namespace Repository.IRepository
{
    public interface IEmployeeRepository
    {
        public Task<Employee> GetEmployee(string email);

        public Employee GetEmployeeByEmailAndPassword(string email, string password);

        public Task InsertEmployee(Employee employee);
        public void UpdateEmployee(Employee employee);
        public void DeleteEmployee(Employee employee);

    }
}