using DemoApp.Application.Common.Interfaces.Repositories;
using ErrorOr;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApp.Application.Salaries.Queries.GetSalaryByEmployeeID
{
    public class GetSalaryByEmployeeIDQueryHandler : IRequestHandler<GetSalaryByEmployeeIDQuery, ErrorOr<GetSalaryByEmployeeIDResult>>
    {
        private readonly ISalaryRepository _salaryRepository;

        public GetSalaryByEmployeeIDQueryHandler(ISalaryRepository salaryRepository)
        {
            _salaryRepository = salaryRepository;
        }

        public async Task<ErrorOr<GetSalaryByEmployeeIDResult>> Handle(GetSalaryByEmployeeIDQuery request, CancellationToken cancellationToken)
        {
            var results = await _salaryRepository.GetSalaryByEmployeeIDAsync(request.Id);

            if (results == null)
            {
                return Error.NotFound("Salary.NotFound", "Salary not found for the given EmployeeID.");
            }

            var salaries = results.Select(salary => new EmployeeSalary(
                salary.SalaryID,
                salary.EmployeeID,
                salary.SalaryAmount,
                salary.Month
                )).ToList();


            return new GetSalaryByEmployeeIDResult(salaries);
        }
    }
}