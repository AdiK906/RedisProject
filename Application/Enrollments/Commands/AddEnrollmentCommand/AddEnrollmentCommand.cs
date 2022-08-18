using Domain.Entities;
using MediatR;
using Redis.OM;
using Redis.OM.Searching;

namespace Application.Enrollments.Commands.AddEnrollmentCommand
{
    public class AddEnrollmentCommand : IRequest<Enrollment>
    {
        public Enrollment? Enrollment { get; set; }

    }

    public class AddEnrollmentCommandHandler : IRequestHandler<AddEnrollmentCommand, Enrollment>
    {
        private readonly RedisCollection<Enrollment> _enrollments;

        public AddEnrollmentCommandHandler(RedisConnectionProvider provider)
        {
            _enrollments = (RedisCollection<Enrollment>)provider.RedisCollection<Enrollment>();
        }

        public async Task<Enrollment> Handle(AddEnrollmentCommand request, CancellationToken cancellationToken)
        {
            _enrollments.Insert(request.Enrollment);

            return request.Enrollment;
        }
    }
}
