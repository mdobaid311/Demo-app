using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using DemoApp.Domain.Users;
using DemoApp.Application.Common.Interfaces.Repositories;

namespace DemoApp.Application.Users.Commands.RegisterUser
{

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository; // Inject your repository or DbContext

        public RegisterUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            // Validate unique email (pseudo-code)
            if (await _userRepository.ExistsAsync(request.Email))
            {
                throw new ArgumentException("Email is already in use.");
            }

            // Hash password (if applicable)
            var hashedPassword = HashPassword(request.Password); // Implement password hashing

            // Map command to User entity
            var user = new User
            {
                UserID = request.UserID,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                PasswordHash = hashedPassword,
                IsDeleted = request.IsDeleted,
                RoleID = request.RoleID,
            };

            // Save user to the database
            await _userRepository.AddAsync(user);

            return user.UserID;
        }

        private string HashPassword(string password)
        {
            // Replace this with a proper password hashing mechanism
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }

}
