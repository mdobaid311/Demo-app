using DemoApp.Application.Common.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Application.Salaries.Commands.DeleteSalary
{
    public class DeleteSalaryCommandHandler : IRequestHandler<DeleteSalaryCommand>
    {
        private readonly ISalaryRepository _salaryRepository;

        public DeleteSalaryCommandHandler(ISalaryRepository salaryRepository)
        {
            _salaryRepository = salaryRepository;
        }

        public async Task Handle(DeleteSalaryCommand request, CancellationToken cancellationToken)
        {
            var salary = await _salaryRepository.SalaryExist(request.Id);
            if(salary == null)
            {
                throw new ArgumentException("Salary does not exist");
            }

            await _salaryRepository.DeleteSalaryAsync(request.Id);
        }
    }
}
