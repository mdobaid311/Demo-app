using DemoApp.Application.Common.Interfaces.Repositories;
using DemoApp.Application.Employees.Commands.UpdateEmployee;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Application.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository; // Inject your repository or DbContext

        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        async Task IRequestHandler<DeleteEmployeeCommand>.Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {

            var employee = await _employeeRepository.GetAsync(request.Id);
            if (employee == null)
            {
                throw new ArgumentException("Employee does not exist");
            }

            await _employeeRepository.DeleteAsync(request.Id);
        }
    }
}
