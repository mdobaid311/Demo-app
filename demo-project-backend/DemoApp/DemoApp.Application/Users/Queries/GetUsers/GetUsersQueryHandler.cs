using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ErrorOr;
using DemoApp.Application.Common.Interfaces.Repositories;

namespace DemoApp.Application.Users.Queries.GetUsers
{
    public class GetUsersqueryHandler : IRequestHandler<GetUsersQuery, ErrorOr<GetUsersQueryResult>>
    {
        private readonly IUserRepository _userRepository; // Inject your repository or DbContext

        public GetUsersqueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<GetUsersQueryResult>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Fetch the list of users from the repository asynchronously
                var results = await _userRepository.GetAllAsync();

                // Map the user data to GetUsersQueryListItem
                var users = results.Select(user => new GetUsersQueryListItem(
                    user.UserID,
                    user.FirstName,
                    user.LastName,
                    user.Email,
                    user.PhoneNumber,
                    user.RoleName
                )).ToList();

                // Return the result wrapped in GetUsersQueryResult
                return new GetUsersQueryResult(users);
            }
            catch (Exception ex)
            {
                // Handle errors (logging, etc.)
                Console.WriteLine($"An error occurred: {ex.Message}");
                // Return the error response (adjust based on how you handle errors)
                return Error.Failure("Error fetching users");
            }
        }
    }
}
