using Domain.Entities;
using MediatR;
using Redis.OM;
using Redis.OM.Searching; 

namespace Application.Students.Queries.GetStudents
{
    public class GetStudentsQuery : IRequest<List<Student>>
    {
        public string? Id { get; set; }
    }

    public class GetStudentQueryHandler : IRequestHandler<GetStudentsQuery, List<Student>>
    {
        private readonly RedisCollection<Student> _students;
        private readonly RedisConnectionProvider _provider;

        public GetStudentQueryHandler(RedisConnectionProvider provider)
        {
            _provider = provider;
            _students = (RedisCollection<Student>)provider.RedisCollection<Student>();
        }

        public async Task<List<Student>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
        {
            return _students.Where(x => x.Id ==request.Id ).ToList();
        }
    }
}// controller w filmiku 2:35:02

