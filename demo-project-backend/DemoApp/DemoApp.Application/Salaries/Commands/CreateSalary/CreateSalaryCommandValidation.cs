using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Application.Salaries.Commands.CreateSalary
{
    public class CreateSalaryCommandValidation : AbstractValidator<CreateSalaryCommand>
    {
        public CreateSalaryCommandValidation() {
            RuleFor(x => x.SalaryAmount).NotEmpty().WithMessage("Salary amount is required.");
            RuleFor(x => x.Month).NotEmpty().WithMessage("Month is required.");
            RuleFor(x => x.EmployeeID).NotEmpty().WithMessage("EmployeeID is required.");
        }
    }
}
