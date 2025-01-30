using DemoApp.Application.Employees.Queries.GetEmployees;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Application.Employees.Queries.GetEmployeeByID
{
    public record GetEmployeeByIDQuery(Guid Id) : IRequest<ErrorOr<GetEmployeeByIDQueryResult>>;

}
