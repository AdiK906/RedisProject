using Domain.Entities;
using MediatR;
using Redis.OM;
using Redis.OM.Searching;

namespace Application.Courses.Commands.AddCourseCommand
{
    public class AddCourseCommand : IRequest<Course>
    {
        public Course? Course { get; set; }

    }

    public class AddCourseCommandHandler : IRequestHandler<AddCourseCommand, Course>
    {
        private readonly RedisCollection<Course> _courses;

        public AddCourseCommandHandler(RedisConnectionProvider provider)
        {
            _courses = (RedisCollection<Course>)provider.RedisCollection<Course>();
        }

        public async Task<Course> Handle(AddCourseCommand request, CancellationToken cancellationToken)
        {
            _courses.Insert(request.Course);

            return request.Course;
        }
    }
}


