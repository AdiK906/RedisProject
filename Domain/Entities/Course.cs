using Redis.OM.Modeling;

namespace Domain.Entities;

[Document(StorageType = StorageType.Json, Prefixes = new[] { "Course" })]
public class Course
{
    [Indexed][RedisIdField]
    public string? Id { get; set; }
    [Indexed]
    public string? Title { get; set; }
    [Indexed]
    public string? Credits { get; set; }

    [Indexed]
    public List<Enrollment>? EnrollmentList { get; set; }
}

