using Domain.Entities;
using MediatR;
using Redis.OM;
using Redis.OM.Searching;

namespace Application.Instructors.Commands.UpdateInstructorsCommand
{
    public class UpdateInstructorCommand : IRequest<Instructor>
    {
        public Instructor? Instructor { get; set; }

    }

    public class AddInstructorCommandHandler : IRequestHandler<UpdateInstructorCommand, Instructor>
    {
        private readonly RedisCollection<Instructor> _instructors;

        public AddInstructorCommandHandler(RedisConnectionProvider provider)
        {
            _instructors = (RedisCollection<Instructor>)provider.RedisCollection<Instructor>();
        }

        public async Task<Instructor> Handle(UpdateInstructorCommand request, CancellationToken cancellationToken)
        {
            await _instructors.UpdateAsync(request.Instructor);
            _instructors.Save();
            return request.Instructor;



            //foreach (var instructor in _instructors.Where(x => x.Id == request.Instructor.Id))
            //{
            //    //instructor.Course.Credits = request.Instructor.Course.Credits;
            //    //instructor.Course.Title = request.Instructor.Course.Title;
            //}
            //_instructors.Save();
            //return request.Instructor;
        }
    }
}
