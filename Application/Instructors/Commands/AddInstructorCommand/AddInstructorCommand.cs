using Domain.Entities;
using MediatR;
using Redis.OM;
using Redis.OM.Searching;

namespace Application.Instructors.Commands.AddInstructorCommand
{
    public class AddInstructorCommand : IRequest<Instructor>
    {
        public Instructor? Instructor { get; set; }

    }

    public class AddInstructorCommandHandler : IRequestHandler<AddInstructorCommand, Instructor>
    {
        private readonly RedisCollection<Instructor> _students;

        public AddInstructorCommandHandler(RedisConnectionProvider provider)
        {
            _students = (RedisCollection<Instructor>)provider.RedisCollection<Instructor>();
        }

        public async Task<Instructor> Handle(AddInstructorCommand request, CancellationToken cancellationToken)
        {
            await _students.InsertAsync(request.Instructor);

            return request.Instructor;
        }
    }
}
