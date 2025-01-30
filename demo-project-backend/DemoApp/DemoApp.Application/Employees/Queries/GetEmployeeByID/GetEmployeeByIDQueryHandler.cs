using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ErrorOr;
using DemoApp.Domain.Employees;
using DemoApp.Application.Common.Interfaces.Repositories;

namespace DemoApp.Application.Employees.Queries.GetEmployeeByID
{
    public class GetEmployeeByIDQueryHandler : IRequestHandler<GetEmployeeByIDQuery, ErrorOr<GetEmployeeByIDQueryResult>>
    {
        private readonly IEmployeeRepository _employeeRepository; // Inject your repository or DbContext

        public GetEmployeeByIDQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<ErrorOr<GetEmployeeByIDQueryResult>> Handle(GetEmployeeByIDQuery request, CancellationToken cancellationToken)
        {
            var result = await _employeeRepository.GetAsync(request.Id);

            if (result == null)
            {
                return new ErrorOr<GetEmployeeByIDQueryResult>();
            }

            return new GetEmployeeByIDQueryResult
                (
                    result.EmployeeID,
                    result.FirstName,
                    result.LastName,
                    result.Email,
                    result.PhoneNumber
                );

        }
    }
}
