using Domain.Entities;
using MediatR;
using Redis.OM;
using Redis.OM.Searching;

namespace Application.Students.Commands
{
    public class AddStudentCommand : IRequest<Student>
    {
        public Student? Student { get; set; }

    }

    public class AddStudentCommandHandler : IRequestHandler<AddStudentCommand, Student>
    {
        private readonly RedisCollection<Student> _students;

        public AddStudentCommandHandler(RedisConnectionProvider provider)
        {
            _students = (RedisCollection<Student>)provider.RedisCollection<Student>();
        }

        public async Task<Student> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            //dobre dodawanie
            await _students.InsertAsync(request.Student);

            //dobre zwracanie 
            return request.Student;
        }
    }
}
