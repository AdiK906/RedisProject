using Domain.Entities;
using MediatR;
using Redis.OM;
using Redis.OM.Searching;

namespace Application.OfficeAssignments.Commands.AddOfficeAssignmentCommand
{
    public class AddOfficeAssignmentCommand : IRequest<OfficeAssignment>
    {
        public OfficeAssignment? OfficeAssignment { get; set; }

    }

    public class AddOfficeAssignmentCommandHandler : IRequestHandler<AddOfficeAssignmentCommand, OfficeAssignment>
    {
        private readonly RedisCollection<OfficeAssignment> _courses;

        public AddOfficeAssignmentCommandHandler(RedisConnectionProvider provider)
        {
            _courses = (RedisCollection<OfficeAssignment>)provider.RedisCollection<OfficeAssignment>();
        }

        public async Task<OfficeAssignment> Handle(AddOfficeAssignmentCommand request, CancellationToken cancellationToken)
        {
            _courses.Insert(request.OfficeAssignment);

            return request.OfficeAssignment;
        }
    }
}
