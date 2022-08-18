using Redis.OM.Modeling;

namespace Domain.Entities;
[Document(StorageType = StorageType.Json, Prefixes = new[] { "Student" })]
public class Student
{

    [Indexed] [RedisIdField]
    public string? Id { get; set; }
    [Indexed]
    public string? LastName { get; set; }
    [Indexed]
    public string? FirstMidName { get; set; }
    [Indexed]
    public DateTime EnrollmentDate { get; set; }
    [Indexed]
    public List<Enrollment>? EnrollmentList { get; set; } 
}
 