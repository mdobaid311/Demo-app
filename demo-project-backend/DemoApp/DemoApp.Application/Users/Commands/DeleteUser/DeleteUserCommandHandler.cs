using DemoApp.Application.Common.Interfaces.Repositories;
using DemoApp.Application.Users.Commands.UpdateUser;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository; // Inject your repository or DbContext

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        async Task IRequestHandler<DeleteUserCommand>.Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {

            var user = await _userRepository.GetAsync(request.Id);
            if (user == null)
            {
                throw new ArgumentException("User does not exist");
            }

            await _userRepository.DeleteAsync(request.Id);
        }
    }
}
