namespace DemoApp.Domain.Employees
{
    public class Employee
    {
        public Guid EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; } = String.Empty;
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public Boolean IsDeleted { get; set; } = false;
    }
}