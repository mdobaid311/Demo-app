using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Application.Employees.Queries.GetEmployees
{
    public record GetEmployeesQueryResult(List<GetEmployeesQueryListItem> Employees);

    public record GetEmployeesQueryListItem(
      Guid EmployeeId,
      string FirstName,
      string LastName,
      string Email,
      string PhoneNumber
        );
}
