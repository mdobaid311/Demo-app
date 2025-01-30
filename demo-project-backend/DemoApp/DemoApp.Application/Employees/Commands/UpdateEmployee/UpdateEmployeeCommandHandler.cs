using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using DemoApp.Domain.Employees;
using DemoApp.Application.Common.Interfaces.Repositories;

namespace DemoApp.Application.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Guid>
    {
        private readonly IEmployeeRepository _employeeRepository; // Inject your repository or DbContext

        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Guid> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            // Validate unique email (pseudo-code)
            if (await _employeeRepository.ExistsAsync(request.Email))
            {
                throw new ArgumentException("Email is already in use.");
            }

            var employee = new Employee
            {
                EmployeeID = request.EmployeeID,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
            };

            await _employeeRepository.UpdateAsync(employee);

            return employee.EmployeeID;
        }
    }
}
