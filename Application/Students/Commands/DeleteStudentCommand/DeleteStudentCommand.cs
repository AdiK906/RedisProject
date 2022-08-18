using Domain.Entities;
using MediatR;
using Redis.OM;
using Redis.OM.Searching;

namespace Application.Students.Commands.DeleteStudentCommand
{
    public class DeleteStudentCommand : IRequest<Student>
    {
        public string? Id { get; set; }
        public Student? Student { get; set; }

    }

    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, Student>
    {
        private readonly RedisConnectionProvider _provider;

        public DeleteStudentCommandHandler(RedisConnectionProvider provider)
        {
            _provider = provider;
        }

        public async Task<Student> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            _provider.Connection.Unlink($"Student:{request.Id}");

            return request.Student;
        }
    }
}
