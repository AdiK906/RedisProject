using Redis.OM.Modeling;

namespace Domain.Entities;

[Document(StorageType = StorageType.Json, Prefixes = new[] { "Instructor" })]
public class Instructor
{
    [Indexed] [RedisIdField]
    public string? Id { get; set; }
    [Indexed]
    public string? FirstMidName { get; set; }
    [Indexed]
    public string? LastName { get; set; }
    public DateTime HireDate { get; set; }
    [Indexed]
    public List<Course>? CoursesList { get; set; }
    [Indexed]
    public List<Department>? DepartmentsList { get; set; }

    [Indexed(CascadeDepth = 1)]
    public OfficeAssignment? OfficeAssignment { get; set; }

}

