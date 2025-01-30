namespace DemoApp.Domain.Users
{
    public class User
    {
        public Guid UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; } = String.Empty;
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public Boolean IsDeleted { get; set; } = false;
        public string RoleName { get; set; }
        public string RoleID { get; set; }
    }
}