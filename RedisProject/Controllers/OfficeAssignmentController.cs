using Application.OfficeAssignments.Commands.AddOfficeAssignmentCommand;
using Application.OfficeAssignments.Commands.AddOfficeAssignmentToInstructorCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RedisProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OfficeAssignmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OfficeAssignmentController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost(Name = "AddOfficeAssignment")]
        public async Task<ActionResult> CreateInstructor([FromBody] AddOfficeAssignmentCommand addOfficeAssignmentCommand)
        {
            return Ok(await _mediator.Send(addOfficeAssignmentCommand));
        }

        //Dodawanie zadań biurowych do instruktorów
        [HttpPost(Name = "AddOfficeAssignmentToInstructor")]
        public async Task<ActionResult> AddOfficeAssignmentToInstructors(string officeAssignmentId, string instructorId)
        {
            var addCourseToInstructorCommand = new AddOfficeAssignmentToInstructorCommand() { InstructorId = instructorId, OfficeAssignmentId = officeAssignmentId };
            await _mediator.Send(addCourseToInstructorCommand);
            return Ok();
        }
    }
}
