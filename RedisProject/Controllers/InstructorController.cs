using Application.Instructors.Commands.AddInstructorCommand;
using Application.Instructors.Commands.UpdateInstructorsCommand;
using Application.Instructors.Queries.GetInstructorByIdQuery;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RedisProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly IMediator _mediator;
        public InstructorController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // Pobieranie studenta
        [HttpGet("{id}")]
        public async Task<ActionResult> GetInstructorById(string id)
        {
            return Ok(await _mediator.Send(new GetInstructorByIdQuery() { Id = id }));
        }

        // Dodawanie instruktora
        [HttpPost(Name = "AddInstructor")]
        public async Task<ActionResult> CreateInstructor([FromBody] AddInstructorCommand addInstructorCommand)
        {
            return Ok(await _mediator.Send(addInstructorCommand));
        }

        // Edycja instruktora
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateInstructor([FromBody] UpdateInstructorCommand updateInstructorCommand)
        {
            await _mediator.Send(updateInstructorCommand);
            return Accepted();
        }
    }
}
