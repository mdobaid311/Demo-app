using DemoApp.Application.Common.Interfaces.Repositories;
using DemoApp.Domain.Salaries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Application.Salaries.Commands.CreateSalary
{
    public class CreateSalaryCommandHandler : IRequestHandler<CreateSalaryCommand,Guid>
    {
        private readonly ISalaryRepository _salaryRepository;

        public CreateSalaryCommandHandler(ISalaryRepository salaryRepository)
        {
            _salaryRepository = salaryRepository;
        }

        public async Task<Guid> Handle(CreateSalaryCommand request, CancellationToken cancellationToken)
        {
            var salary = new Salary
            {
                SalaryID = request.SalaryID,
                EmployeeID = request.EmployeeID,
                SalaryAmount = request.SalaryAmount,
                Month = request.Month
            };

            await _salaryRepository.CreateSalaryAsync(salary);

            return request.SalaryID;
        }
    }
}
