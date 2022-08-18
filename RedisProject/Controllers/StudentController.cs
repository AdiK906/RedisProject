using Application.Enrollments.Commands.AddEnrollmentCommand;
using Application.Enrollments.Commands.AddEnrollmentToListOfStudentsCommand;
using Application.Students.Commands;
using Application.Students.Commands.DeleteStudentCommand;
using Application.Students.Commands.UpdateStudentCommand;
using Application.Students.Queries.GetStudents;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Redis.OM;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RedisProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StudentController(IMediator mediator, RedisConnectionProvider provider)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetStudentById(string id)
        {
            return Ok( await _mediator.Send(new GetStudentsByIdQuery() { Id = id }));
        }

        // POST api/<StudentController>
        [HttpPost(Name = "AddStudent")]
        public async Task<ActionResult> CreateStudent([FromBody] AddStudentCommand addStudentCommand)
        {
            var result = await _mediator.Send(addStudentCommand);
            return Ok(result);
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStudent([FromBody] UpdateStudentCommand updateStudentCommand)
        {
            await _mediator.Send(updateStudentCommand);
            return Accepted();
        }

        // DELETE student
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudent(string id)
        {
            var deletedStudentCommand = new DeleteStudentCommand() { Id = id };
            await _mediator.Send(deletedStudentCommand);

            return NoContent();
        }

    }
}
