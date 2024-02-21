namespace Domain.Model
{
    public class TimeRegister
    {
        public Employee Employee { get; set; }
        public DateTime EmployeeTimeRegister { get; set; }
        public int Turn { get; set; }
    }
}