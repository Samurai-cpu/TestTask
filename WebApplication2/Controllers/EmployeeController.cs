using Application.Commands;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id, CancellationToken token)
        {
            var query = new GetEmployeeByIdQuery { Id = id};
            var employee = await _mediator.Send(query, token);
            return Ok(employee);
        }

        [HttpPut]
        public async Task<IActionResult> EnableEmployee(ChangeEmployeeEnabledStatusCommand command, CancellationToken token)
        {
            await _mediator.Send(command, token);
            return NoContent();
        }
    }
}