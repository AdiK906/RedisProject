using Domain.Entities;
using MediatR;
using Redis.OM;
using Redis.OM.Searching;

namespace Application.Students.Commands.UpdateStudentCommand
{
    public class UpdateStudentCommand : IRequest<Student>
    {
        public Student? Student { get; set; } 

    }

    public class DeleteStudentCommandHandler : IRequestHandler<UpdateStudentCommand, Student>
    {
        private readonly RedisCollection<Student> _students;

        public DeleteStudentCommandHandler(RedisConnectionProvider provider)
        {
            _students = (RedisCollection<Student>)provider.RedisCollection<Student>();
        }

        public async Task<Student> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            //update asynch
            await _students.UpdateAsync(request.Student);
            _students.Save();
            return request.Student;

            //foreach (var student in _students.Where(x => x.Id == request.Student.Id))
            //{
            //    student.FirstMidName = request.Student.FirstMidName;
            //    student.LastName = request.Student.LastName;
            //}
            //await _students.SaveAsync();
            //return request.Student;
        }
    }
}
