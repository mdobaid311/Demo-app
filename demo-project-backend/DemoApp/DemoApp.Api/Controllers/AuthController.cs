using DemoApp.Api.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DemoApp.Contracts.Users;
using DemoApp.Application.Users.Commands.RegisterUser;
using MediatR;
using System.Numerics;
using DemoApp.Application.Common.Interfaces;

namespace DemoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(ISender _mediator) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterUserRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request data.");
            }

            var command = new RegisterUserCommand
            {
                UserID = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Password = request.Password,
                RoleID = request.RoleID,
            };

            try
            {
                var userId = await _mediator.Send(command);
                return Ok(new { UserId = userId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loginCommand = new LoginUserCommand
            {
                Email = request.Email,
                Password = request.Password
            };

            try
            {
                var data = await _mediator.Send(loginCommand);

                return Ok(data);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid email or password.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                // Log the exception here for debugging purposes
                return Problem("An unexpected error occurred.", null, 500, "Internal Server Error");
            }
        }
    }
}
