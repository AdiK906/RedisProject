using Domain.Entities;
using MediatR;
using Redis.OM;
using Redis.OM.Searching;

namespace Application.OfficeAssignments.Commands.AddOfficeAssignmentToInstructorCommand
{
    public class AddOfficeAssignmentToInstructorCommand : IRequest<Instructor>
    {
        public string? InstructorId { get; set; }
        public string? OfficeAssignmentId { get; set; }

    }

    public class AddOfficeAssignmentToInstructorCommandHandler : IRequestHandler<AddOfficeAssignmentToInstructorCommand, Instructor>
    {
        private readonly RedisCollection<OfficeAssignment> _officeAssignments;
        private readonly RedisCollection<Instructor> _instructors;

        public AddOfficeAssignmentToInstructorCommandHandler(RedisConnectionProvider provider)
        {
            _officeAssignments = (RedisCollection<OfficeAssignment>)provider.RedisCollection<OfficeAssignment>();
            _instructors = (RedisCollection<Instructor>)provider.RedisCollection<Instructor>();
        }

        public async Task<Instructor> Handle(AddOfficeAssignmentToInstructorCommand request, CancellationToken cancellationToken)
        {
            var office = await _officeAssignments.FindByIdAsync(request.OfficeAssignmentId);
            var instructor = await _instructors.FindByIdAsync(request.InstructorId);

            instructor.OfficeAssignment = office;
            await _instructors.UpdateAsync(instructor);

            _instructors.Save();

            return instructor;
        }
    }
}
