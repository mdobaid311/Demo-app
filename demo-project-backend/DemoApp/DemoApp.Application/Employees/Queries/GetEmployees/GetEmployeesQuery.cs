using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Application.Employees.Queries.GetEmployees
{
    public record GetEmployeesQuery() : IRequest<ErrorOr<GetEmployeesQueryResult>>;
}
