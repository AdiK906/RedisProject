using Application.Enrollments.Commands.AddEnrollmentCommand;
using Application.Enrollments.Commands.AddEnrollmentToListOfCoursesCommand;
using Application.Enrollments.Commands.AddEnrollmentToListOfStudentsCommand;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RedisProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EnrollmentController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        //Dodawanie zapisów
        [HttpPost(Name = "AddEnrollment")]
        public async Task<ActionResult> CreateEnrollment([FromBody] AddEnrollmentCommand addEnrollmentCommand)
        {
            var result = await _mediator.Send(addEnrollmentCommand);
            return Ok(result);
        }

        //Dodawanie listy zapisów do studentów
        [HttpPost(Name = "AddEnrollmentToListOfStudents")]
        public async Task<ActionResult> AddEnrollmentToListOfStudents(string studentId, string enrollmentId)
        {
            var addEnrollmentToListOfStudentsCommand = new AddEnrollmentToListOfStudentsCommand() {StudentId = studentId, EnrollmentId = enrollmentId};
            await _mediator.Send(addEnrollmentToListOfStudentsCommand);
            return Ok();
        }

        //Dodawanie listy zapisów do kursów
        [HttpPost(Name = "AddEnrollmentToListOfCourses")]
        public async Task<ActionResult> AddEnrollmentToListOfCourses(string courseId, string enrollmentId)
        {
            var addEnrollmentToListOfCoursesCommand = new AddEnrollmentToListOfCoursesCommand() { CourseId = courseId, EnrollmentId = enrollmentId };
            await _mediator.Send(addEnrollmentToListOfCoursesCommand);
            return Ok();
        }
    }
}
