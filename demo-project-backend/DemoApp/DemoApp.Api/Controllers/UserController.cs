using DemoApp.Api.Controllers;
using DemoApp.Application.Users.Commands.UpdateUser;
using DemoApp.Application.Users.Queries.GetUserByID;
using DemoApp.Application.Users.Queries.GetUsers;
using DemoApp.Contracts.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : ApiController
    {
        private readonly ISender _mediator;

        // Constructor for injecting ISender
        public UserController(ISender mediator)
        {
            _mediator = mediator;
        }

        // Route to get all users
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var query = new GetUsersQuery();

            try
            {
                var users = await _mediator.Send(query);
                return Ok(users.Value.Users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Route to get a user by id
        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var query = new GetUserByIDQuery(new Guid(id));

            try
            {
                var users = await _mediator.Send(query);
                return Ok(users.Value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Route to update user
        [HttpPut("users")]
        public async Task<IActionResult> UpdateUser(UpdateUserRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request data.");
            }

            var command = new UpdateUserCommand
            {
                UserID = request.UserId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
            };

            try
            {
                var userId = await _mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var command = new DeleteUserCommand(new Guid(id));

            try
            {
                await _mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
