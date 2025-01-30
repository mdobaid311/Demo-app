using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ErrorOr;
using DemoApp.Application.Common.Interfaces.Repositories;

namespace DemoApp.Application.Employees.Queries.GetEmployees
{
    public class GetEmployeesqueryHandler : IRequestHandler<GetEmployeesQuery, ErrorOr<GetEmployeesQueryResult>>
    {
        private readonly IEmployeeRepository _employeeRepository; // Inject your repository or DbContext

        public GetEmployeesqueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<ErrorOr<GetEmployeesQueryResult>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Fetch the list of employees from the repository asynchronously
                var results = await _employeeRepository.GetAllAsync();

                // Map the employee data to GetEmployeesQueryListItem
                var employees = results.Select(employee => new GetEmployeesQueryListItem(
                    employee.EmployeeID,
                    employee.FirstName,
                    employee.LastName,
                    employee.Email,
                    employee.PhoneNumber
                )).ToList();

                // Return the result wrapped in GetEmployeesQueryResult
                return new GetEmployeesQueryResult(employees);
            }
            catch (Exception ex)
            {
                // Handle errors (logging, etc.)
                Console.WriteLine($"An error occurred: {ex.Message}");
                // Return the error response (adjust based on how you handle errors)
                return Error.Failure("Error fetching employees");
            }
        }
    }
}
