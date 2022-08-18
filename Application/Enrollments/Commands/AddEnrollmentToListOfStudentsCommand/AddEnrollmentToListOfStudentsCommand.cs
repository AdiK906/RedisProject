using Domain.Entities;
using MediatR;
using Redis.OM;
using Redis.OM.Searching;

namespace Application.Enrollments.Commands.AddEnrollmentToListOfStudentsCommand
{
    public class AddEnrollmentToListOfStudentsCommand : IRequest<Student>
    {
        public string? EnrollmentId { get; set; }
        public string? StudentId { get; set; }

    }

    public class AddEnrollmentToListCommandHandler : IRequestHandler<AddEnrollmentToListOfStudentsCommand, Student>
    {
        private readonly RedisCollection<Student> _students;
        private readonly RedisCollection<Enrollment> _enrollments;

        public AddEnrollmentToListCommandHandler(RedisConnectionProvider provider)
        {
            _students = (RedisCollection<Student>)provider.RedisCollection<Student>();
            _enrollments = (RedisCollection<Enrollment>)provider.RedisCollection<Enrollment>();
        }

        public async Task<Student> Handle(AddEnrollmentToListOfStudentsCommand request, CancellationToken cancellationToken)
        {
            var example = await _students.FindByIdAsync(request.StudentId);
            var enroll = await _enrollments.FindByIdAsync(request.EnrollmentId);

            if (example.EnrollmentList.FirstOrDefault(x => x.Id == request.EnrollmentId) == null)
            {
                example.EnrollmentList.Add(enroll);
                await _students.UpdateAsync(example);

                _students.Save();
            }
            else
            {
                throw new ApplicationException("This object is already added");
            }

            return example;
        }
    }
}
