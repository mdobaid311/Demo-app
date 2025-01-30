using DemoApp.Domain.Salaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Application.Common.Interfaces.Repositories
{
    public interface ISalaryRepository
    {
        Task<List<Salary>> GetSalaryByEmployeeIDAsync(Guid employeeID);
        Task<bool> SalaryExist(Guid salaryID);
        Task<Salary> CreateSalaryAsync(Salary salary);
        Task DeleteSalaryAsync(Guid salaryID);
    }
}
