using Domain.Model;

namespace Repository.IRepository
{
    public interface IEmployeeTimeRegister
    {
        public void RegisterTime(TimeRegister timeRegister);
        public TimeRegister GetTimeRegisterByEmployeeAndDate (Employee employee, TimeRegister timeRegister);
    }
}