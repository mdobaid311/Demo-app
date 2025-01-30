using System;

namespace DemoApp.Contracts.Employees
{
    public record CreateEmployeeRequest(
        string FirstName,
        string LastName,
        string PhoneNumber,
        string Email
    );
}
