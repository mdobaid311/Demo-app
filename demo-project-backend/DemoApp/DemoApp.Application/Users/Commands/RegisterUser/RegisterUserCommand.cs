using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApp.Application.Common.Security.Request;

namespace DemoApp.Application.Users.Commands.RegisterUser
{
    public record RegisterUserCommand : IAuthorizableRequest<Guid>
    {
        public Guid UserID { get; init; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required string PhoneNumber { get; init; }
        public required string Email { get; init; }
        public required string Password { get; init; }
        public required string RoleID { get; init; }
        public bool IsDeleted { get; init; }
    }
}
