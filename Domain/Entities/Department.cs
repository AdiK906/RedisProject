using Redis.OM.Modeling;

namespace Domain.Entities;

[Document(StorageType = StorageType.Json, Prefixes = new[] { "Department" })]
public class Department
{
    [Indexed][RedisIdField]
    public string? Id { get; set; }
    [Indexed]
    public string? Name { get; set; }
    [Indexed]
    public string? Budget { get; set; }
    public DateTime StartDate { get; set; }
    [Indexed]
    public List<Course>? CoursesList { get; set; }

}

