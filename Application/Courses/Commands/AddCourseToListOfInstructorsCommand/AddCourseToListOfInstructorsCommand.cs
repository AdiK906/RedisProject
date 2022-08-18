using Domain.Entities;
using MediatR;
using Redis.OM;
using Redis.OM.Searching;

namespace Application.Courses.Commands.AddCourseToListOfInstructorsCommand
{
    public class AddCourseToListOfInstructorsCommand : IRequest<Instructor>
    {
        public string? InstructorId { get; set; }
        public string? CourseId { get; set; }

    }

    public class AddCourseToListCommandHandler : IRequestHandler<AddCourseToListOfInstructorsCommand, Instructor>
    {
        private readonly RedisCollection<Course> _courses;
        private readonly RedisCollection<Instructor> _instructors;

        public AddCourseToListCommandHandler(RedisConnectionProvider provider)
        {
            _courses = (RedisCollection<Course>)provider.RedisCollection<Course>();
            _instructors = (RedisCollection<Instructor>)provider.RedisCollection<Instructor>();
        }

        public async Task<Instructor> Handle(AddCourseToListOfInstructorsCommand request, CancellationToken cancellationToken)
        {
            var instructor = await _instructors.FindByIdAsync(request.InstructorId);
            var course = await _courses.FindByIdAsync(request.CourseId);

            if (instructor.CoursesList.FirstOrDefault(x => x.Id == request.CourseId) == null)
            {
                instructor.CoursesList.Add(course);
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
