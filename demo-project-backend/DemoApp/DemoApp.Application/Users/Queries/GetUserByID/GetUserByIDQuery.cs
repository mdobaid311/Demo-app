using DemoApp.Application.Users.Queries.GetUsers;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Application.Users.Queries.GetUserByID
{
    public record GetUserByIDQuery(Guid Id) : IRequest<ErrorOr<GetUserByIDQueryResult>>;

}
