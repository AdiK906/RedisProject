using Application.Courses.Commands.AddCourseCommand;
using Application.Courses.Commands.AddCourseToListOfInstructorsCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RedisProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CourseController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        //Dodanie kursu
        [HttpPost(Name = "AddCourse")]
        public async Task<ActionResult> CreateCourse([FromBody] AddCourseCommand addCourseCommand)
        {
            var result = await _mediator.Send(addCourseCommand);
            return Ok(result);
        }

        //Dodawanie listy kursów do instruktorów
        [HttpPost(Name = "AddCourseToListOfInstructors")]
        public async Task<ActionResult> AddCourseToListOfInstructors(string courseId, string instructorId)
        {
            var addCourseToListOfInstructorsCommand = new AddCourseToListOfInstructorsCommand() { InstructorId = instructorId, CourseId = courseId};
            await _mediator.Send(addCourseToListOfInstructorsCommand);
            return Ok();
        }
    }
}
