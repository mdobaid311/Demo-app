using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Contracts.Users
{
    public record UpdateUserRequest
    (
        Guid UserId,
        string FirstName,
        string LastName,
        string PhoneNumber,
        string Email
    );
}
