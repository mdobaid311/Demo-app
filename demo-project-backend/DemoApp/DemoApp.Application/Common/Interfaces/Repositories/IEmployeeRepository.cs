using DemoApp.Domain.Employees;
using System.Linq.Expressions;

namespace DemoApp.Application.Common.Interfaces.Repositories;

public interface IEmployeeRepository
{
    Task AddAsync(Employee user);
    Task UpdateAsync(Employee user);
    Task DeleteAsync(Guid id);
    Task<Employee> GetAsync(Guid id);
    Task<Employee> GetByEmailAsync(string Email);
    Task<List<Employee>> GetAllAsync();
    Task<bool> ExistsAsync(string Email);
}