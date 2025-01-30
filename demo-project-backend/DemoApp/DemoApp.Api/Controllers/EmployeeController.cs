using DemoApp.Api.Controllers;
using DemoApp.Application.Employees.Commands.CreateEmployee;
using DemoApp.Application.Employees.Commands.UpdateEmployee;
using DemoApp.Application.Employees.Queries.GetEmployeeByID;
using DemoApp.Application.Employees.Queries.GetEmployees;
using DemoApp.Application.Users.Commands.RegisterUser;
using DemoApp.Contracts.Employees;
using DemoApp.Contracts.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class EmployeeController : ApiController
    {
        private readonly ISender _mediator;

        // Constructor for injecting ISender
        public EmployeeController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("employee")]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request data.");
            }

            var command = new CreateEmployeeCommand
            {
                EmployeeID = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
            };

            try
            {
                var employeeId = await _mediator.Send(command);
                return Ok(new { EmployeeId = employeeId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // Route to get all employee
        [HttpGet("employee")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var query = new GetEmployeesQuery();

            try
            {
                var employee = await _mediator.Send(query);
                return Ok(employee.Value.Employees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Route to get a user by id
        [HttpGet("employee/{id}")]
        public async Task<IActionResult> GetEmployee(string id)
        {
            var query = new GetEmployeeByIDQuery(new Guid(id));

            try
            {
                var employee = await _mediator.Send(query);
                return Ok(employee.Value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Route to update user
        [HttpPut("employee")]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request data.");
            }

            var command = new UpdateEmployeeCommand
            {
                EmployeeID = request.EmployeeId,
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

        [HttpDelete("employee/{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            var command = new DeleteEmployeeCommand(new Guid(id));

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
