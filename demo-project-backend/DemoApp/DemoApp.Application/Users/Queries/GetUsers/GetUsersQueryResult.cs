using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Application.Users.Queries.GetUsers
{
    public record GetUsersQueryResult(List<GetUsersQueryListItem> Users);

    public record GetUsersQueryListItem(
      Guid UserId,
      string FirstName,
      string LastName,
      string Email,
      string PhoneNumber,
      string RoleName
        );
}
