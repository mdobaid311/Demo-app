using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DemoApp.Application.Common.DTOs;
using DemoApp.Application.Common.Interfaces;
using DemoApp.Application.Common.Interfaces.Repositories;
using DemoApp.Domain.Users;
using MediatR;

namespace DemoApp.Application.Users.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResponseDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenGenerator _tokenGenerator;

        public LoginUserCommandHandler(IUserRepository userRepository, ITokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<AuthResponseDTO> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            string token = _tokenGenerator.GenerateJWTToken(user.UserID, user.FirstName, user.LastName);

            return new AuthResponseDTO()
            {
                UserId = user.UserID,
                Name = user.FirstName+ " " + user.LastName,
                RoleName = user.RoleName,
                Email = user.Email,
                Token = token
            };
        }

        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, storedHash);
        }

    }
}
