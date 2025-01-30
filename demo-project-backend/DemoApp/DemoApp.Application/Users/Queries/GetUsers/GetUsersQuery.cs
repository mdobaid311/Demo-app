using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Application.Users.Queries.GetUsers
{
    public record GetUsersQuery() : IRequest<ErrorOr<GetUsersQueryResult>>;
}
