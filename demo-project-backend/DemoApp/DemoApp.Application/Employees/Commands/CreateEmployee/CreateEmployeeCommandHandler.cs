using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using DemoApp.Domain.Employees;
using DemoApp.Application.Common.Interfaces.Repositories;

namespace DemoApp.Application.Employees.Commands.CreateEmployee
{

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Guid>
    {
        private readonly IEmployeeRepository _employeeRepository; // Inject your repository or DbContext

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            // Validate unique email (pseudo-code)
            if (await _employeeRepository.ExistsAsync(request.Email))
            {
                throw new ArgumentException("Email is already in use.");
            }

            // Map command to Employee entity
            var employee = new Employee
            {
                EmployeeID = request.EmployeeID,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                IsDeleted = request.IsDeleted
            };

            // Save employee to the database
            await _employeeRepository.AddAsync(employee);

            return employee.EmployeeID;
        }

        private string HashPassword(string password)
        {
            // Replace this with a proper password hashing mechanism
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }

}
