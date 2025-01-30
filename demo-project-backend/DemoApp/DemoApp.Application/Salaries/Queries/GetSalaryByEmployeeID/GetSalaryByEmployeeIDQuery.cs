using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Application.Salaries.Queries.GetSalaryByEmployeeID
{
    public record GetSalaryByEmployeeIDQuery(Guid Id) :IRequest<ErrorOr<GetSalaryByEmployeeIDResult>>;
}
