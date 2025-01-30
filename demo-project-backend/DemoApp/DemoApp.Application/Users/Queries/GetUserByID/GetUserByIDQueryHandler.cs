using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ErrorOr;
using DemoApp.Domain.Users;
using DemoApp.Application.Common.Interfaces.Repositories;

namespace DemoApp.Application.Users.Queries.GetUserByID
{
    public class GetUserByIDQueryHandler : IRequestHandler<GetUserByIDQuery, ErrorOr<GetUserByIDQueryResult>>
    {
        private readonly IUserRepository _userRepository; // Inject your repository or DbContext

        public GetUserByIDQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<GetUserByIDQueryResult>> Handle(GetUserByIDQuery request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetAsync(request.Id);

            if (result == null)
            {
                return new ErrorOr<GetUserByIDQueryResult>();
            }

            return new GetUserByIDQueryResult
                (
                    result.UserID,
                    result.FirstName,
                    result.LastName,
                    result.Email,
                    result.PhoneNumber
                );

        }
    }
}
