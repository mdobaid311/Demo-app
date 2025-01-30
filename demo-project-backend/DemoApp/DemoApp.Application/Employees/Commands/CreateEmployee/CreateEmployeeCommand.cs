using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApp.Application.Common.Security.Request;
using MediatR;

namespace DemoApp.Application.Employees.Commands.CreateEmployee
{
    public record CreateEmployeeCommand : IRequest<Guid>
    {
        public Guid EmployeeID { get; init; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required string PhoneNumber { get; init; }
        public required string Email { get; init; }
        public bool IsDeleted { get; init; }
    }
}
