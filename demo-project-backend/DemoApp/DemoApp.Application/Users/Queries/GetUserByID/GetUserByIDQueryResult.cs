using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Application.Users.Queries.GetUserByID
{
    public record GetUserByIDQueryResult
   (
        Guid UserId,
      string FirstName,
      string LastName,
      string Email,
      string PhoneNumber
    );
}
