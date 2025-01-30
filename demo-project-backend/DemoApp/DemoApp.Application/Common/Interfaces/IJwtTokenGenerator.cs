using DemoApp.Domain.Users;

namespace DemoApp.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(
        Guid id,
        string firstName,
        string lastName,
        string email);
}