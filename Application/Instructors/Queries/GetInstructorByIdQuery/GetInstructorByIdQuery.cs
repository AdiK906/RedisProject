using Domain.Entities;
using MediatR;
using Redis.OM;
using Redis.OM.Searching;


namespace Application.Instructors.Queries.GetInstructorByIdQuery
{
    public class GetInstructorByIdQuery : IRequest<Instructor>
    {
        public string? Id { get; set; }
    }

    public class AddInstructorCommandHandler : IRequestHandler<GetInstructorByIdQuery, Instructor>
    {
        private readonly RedisCollection<Instructor> _instructors;

        public AddInstructorCommandHandler(RedisConnectionProvider provider)
        {
            _instructors = (RedisCollection<Instructor>)provider.RedisCollection<Instructor>();
        }

        public async Task<Instructor> Handle(GetInstructorByIdQuery request, CancellationToken cancellationToken)
        {
            return await _instructors.FindByIdAsync(request.Id);
        }
    }
}
