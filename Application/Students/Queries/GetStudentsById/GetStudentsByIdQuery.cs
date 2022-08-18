using Domain.Entities;
using MediatR;
using Redis.OM.Searching;
using Redis.OM;

namespace Application.Students.Queries.GetStudents
{
    public class GetStudentsByIdQuery : IRequest<Student> // tu ma być imo lista
    {
        public string? Id { get; set; }

    }

    public class GetStudentsByIdQueryHandler : IRequestHandler<GetStudentsByIdQuery, Student>
    {
        private readonly RedisCollection<Student> _students;


        public GetStudentsByIdQueryHandler(RedisConnectionProvider provider)
        {
            _students = (RedisCollection<Student>)provider.RedisCollection<Student>();
        }

        public async Task<Student> Handle(GetStudentsByIdQuery request, CancellationToken cancellationToken)
        {
            //asynchroniczne rozwiazanie
            return await _students.FindByIdAsync(request.Id);

            //synchroniczne rozwiązanie
            //return _students.Where(x => x.Id == request.Id).ToList();
        }
    }
}
