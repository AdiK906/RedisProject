using Domain.Entities;
using MediatR;
using Redis.OM;
using Redis.OM.Searching;

namespace Application.Departments.Commands.AddDepartmentCommand
{
    public class AddDepartmentCommand : IRequest<Department>
    {
        public Department? Department { get; set; }

    }

    public class AddDepartmentCommandHandler : IRequestHandler<AddDepartmentCommand, Department>
    {
        private readonly RedisCollection<Department> _departments;

        public AddDepartmentCommandHandler(RedisConnectionProvider provider)
        {
            _departments = (RedisCollection<Department>)provider.RedisCollection<Department>();
        }

        public async Task<Department> Handle(AddDepartmentCommand request, CancellationToken cancellationToken)
        {
            _departments.Insert(request.Department);

            return request.Department;
        }
    }
}
