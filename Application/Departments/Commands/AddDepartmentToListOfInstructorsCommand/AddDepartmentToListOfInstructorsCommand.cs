using Domain.Entities;
using MediatR;
using Redis.OM;
using Redis.OM.Searching;

namespace Application.Departments.Commands.AddDepartmentToListOfInstructorsCommand
{
    public class AddDepartmentToListOfInstructorsCommand : IRequest<Instructor>
    {
        public string? DepartmentId { get; set; }
        public string? InstructorId { get; set; }

    }

    public class AddDepartmentToListOfInstructorsCommandHandler : IRequestHandler<AddDepartmentToListOfInstructorsCommand, Instructor>
    {
        private readonly RedisCollection<Instructor> _instructors;
        private readonly RedisCollection<Department> _departments;

        public AddDepartmentToListOfInstructorsCommandHandler(RedisConnectionProvider provider)
        {
            _instructors = (RedisCollection<Instructor>)provider.RedisCollection<Instructor>();
            _departments = (RedisCollection<Department>)provider.RedisCollection<Department>();
        }

        public async Task<Instructor> Handle(AddDepartmentToListOfInstructorsCommand request, CancellationToken cancellationToken)
        {
            var instructor = await _instructors.FindByIdAsync(request.InstructorId);
            var department = await _departments.FindByIdAsync(request.DepartmentId);

            if (instructor.DepartmentsList.FirstOrDefault(x => x.Id == request.DepartmentId) == null)
            {
                instructor.DepartmentsList.Add(department);
                await _instructors.UpdateAsync(instructor);

                _instructors.Save();
            }
            else
            {
                throw new ApplicationException("This object is already added");
            }

            return instructor;
        }
    }
}
