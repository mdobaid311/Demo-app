using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Contracts.Salaries
{
    public record CreateSalaryRequest(
        string EmployeeID,
        decimal SalaryAmount,
        string Month
        );
}
