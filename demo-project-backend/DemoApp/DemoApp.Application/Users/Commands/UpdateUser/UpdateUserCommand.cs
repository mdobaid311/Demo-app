using DemoApp.Application.Common.Security.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Application.Users.Commands.UpdateUser
{
    public record UpdateUserCommand : IAuthorizableRequest<Guid>
    {
        public Guid UserID { get; init; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required string PhoneNumber { get; init; }
        public required string Email { get; init; }
    }
}
