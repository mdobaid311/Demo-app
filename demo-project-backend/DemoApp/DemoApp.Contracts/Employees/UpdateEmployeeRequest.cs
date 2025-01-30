using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Contracts.Employees
{
    public record UpdateEmployeeRequest
    (
        Guid EmployeeId,
        string FirstName,
        string LastName,
        string PhoneNumber,
        string Email
    );
}
