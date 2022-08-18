using Domain.Entities;
using MediatR;
using Redis.OM;
using Redis.OM.Searching;

namespace Application.Enrollments.Commands.AddEnrollmentToListOfCoursesCommand
{
    public class AddEnrollmentToListOfCoursesCommand : IRequest<Course>
    {
        public string? EnrollmentId { get; set; }
        public string? CourseId { get; set; }

    }

    public class AddEnrollmentToListOfCoursesCommandHandler : IRequestHandler<AddEnrollmentToListOfCoursesCommand, Course>
    {
        private readonly RedisCollection<Course> _courses;
        private readonly RedisCollection<Enrollment> _enrollments;

        public AddEnrollmentToListOfCoursesCommandHandler(RedisConnectionProvider provider)
        {
            _courses = (RedisCollection<Course>)provider.RedisCollection<Course>();
            _enrollments = (RedisCollection<Enrollment>)provider.RedisCollection<Enrollment>();
        }

        public async Task<Course> Handle(AddEnrollmentToListOfCoursesCommand request, CancellationToken cancellationToken)
        {
            var course = await _courses.FindByIdAsync(request.CourseId);
            var enroll = await _enrollments.FindByIdAsync(request.EnrollmentId);

            if (course.EnrollmentList.FirstOrDefault(x => x.Id == request.EnrollmentId) == null)
            {
                course.EnrollmentList.Add(enroll);
                await _courses.UpdateAsync(course);

                _courses.Save();
            }
            else
            {
                throw new ApplicationException("This object is already added");
            }

            return course;
        }
    }
}
