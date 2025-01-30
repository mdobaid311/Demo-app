using DemoApp.Application.Common.Security.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Application.Salaries.Commands.CreateSalary
{
    public record CreateSalaryCommand : IRequest<Guid>
    {
        public Guid SalaryID { get; set; }
        public Guid EmployeeID { get; set; }
        public decimal SalaryAmount { get; set; }
        public string Month { get; set; }
    }
}
