using MediatR;
using DemoApp.Domain.Users;
using DemoApp.Application.Common.DTOs;

public class LoginUserCommand : IRequest<AuthResponseDTO> // The JWT token will be returned as a string
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
