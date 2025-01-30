using DemoApp.Domain.Users;
using System.Linq.Expressions;

namespace DemoApp.Application.Common.Interfaces.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(Guid id);
    Task<User> GetAsync(Guid id);
    Task<User> GetByEmailAsync(string Email);
    Task<List<User>> GetAllAsync();
    Task<bool> ExistsAsync(string Email);
}