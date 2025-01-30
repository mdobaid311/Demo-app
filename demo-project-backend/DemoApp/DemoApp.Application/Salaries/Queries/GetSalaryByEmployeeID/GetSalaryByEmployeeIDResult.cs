using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Application.Salaries.Queries.GetSalaryByEmployeeID
{
    public record GetSalaryByEmployeeIDResult(List<EmployeeSalary> salaries);

    public record EmployeeSalary(Guid SalaryID,
        Guid EmployeeID,
        decimal SalaryAmount,
        string Month);
}
