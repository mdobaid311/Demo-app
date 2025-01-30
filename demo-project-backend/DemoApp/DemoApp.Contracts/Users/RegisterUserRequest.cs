using System;

namespace DemoApp.Contracts.Users
{
    public record RegisterUserRequest(
        string FirstName,
        string LastName,
        string PhoneNumber,
        string Email,
        string Password,
        string RoleID
    );
}
