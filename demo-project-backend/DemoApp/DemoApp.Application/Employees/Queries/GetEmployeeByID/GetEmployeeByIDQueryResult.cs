using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Application.Employees.Queries.GetEmployeeByID
{
    public record GetEmployeeByIDQueryResult
   (
        Guid EmployeeId,
      string FirstName,
      string LastName,
      string Email,
      string PhoneNumber
    );
}
