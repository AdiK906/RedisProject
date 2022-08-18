using Application.Departments.Commands.AddDepartmentCommand;
using Application.Departments.Commands.AddDepartmentToListOfInstructorsCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RedisProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        //Dodanie działu
        [HttpPost(Name = "AddDepartment")]
        public async Task<ActionResult> AddDepartment([FromBody] AddDepartmentCommand addDepartmentCommand)
        {
            return Ok(await _mediator.Send(addDepartmentCommand));
        }

        //Dodawanie działu do instruktorów
        [HttpPost(Name = "AddDepartmentToListOfInstructors")]
        public async Task<ActionResult> AddDepartmentToListOfInstructors(string departmentId, string instructorId)
        {
            var addDepartmentToListOfInstructorCommand = new AddDepartmentToListOfInstructorsCommand() { InstructorId = instructorId, DepartmentId = departmentId};
            await _mediator.Send(addDepartmentToListOfInstructorCommand);
            return Ok();
        }
    }
}
