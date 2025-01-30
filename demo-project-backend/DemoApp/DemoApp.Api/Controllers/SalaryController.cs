using DemoApp.Application.Employees.Queries.GetEmployeeByID;
using DemoApp.Application.Salaries.Commands.CreateSalary;
using DemoApp.Application.Salaries.Commands.DeleteSalary;
using DemoApp.Application.Salaries.Queries.GetSalaryByEmployeeID;
using DemoApp.Contracts.Salaries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class SalaryController : ApiController
    {
        private readonly ISender _mediator;

        public SalaryController(ISender mediator)
        {
            _mediator = mediator;
        }

        // Route to get a user by id
        [HttpGet("salary/{employeeID}")]
        public async Task<IActionResult> GetSalary(string employeeID)
        {
            var query = new GetSalaryByEmployeeIDQuery(new Guid(employeeID));

            try
            {
                var salary = await _mediator.Send(query);
                return Ok(salary.Value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("salary")]
        public async Task<IActionResult> CreateSalary(CreateSalaryRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request data.");
            }

            var command = new CreateSalaryCommand
            {
                SalaryID = Guid.NewGuid(),
                Month = request.Month,
                EmployeeID = new Guid(request.EmployeeID),
                SalaryAmount = request.SalaryAmount,
            };

            try
            {
                var salaryID = await _mediator.Send(command);
                return Ok(new { SalaryID = salaryID });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("salary/{salaryID}")]
        public async Task<IActionResult> DeleteSalary(string salaryID)
        {
            var command = new DeleteSalaryCommand(new Guid(salaryID));

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