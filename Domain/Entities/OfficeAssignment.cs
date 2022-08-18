using Redis.OM.Modeling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

[Document(StorageType = StorageType.Json, Prefixes = new[] { "OfficeAssignments" })]
public class OfficeAssignment
{
    [Indexed][RedisIdField]
    public string? Id { get; set; }
    [Indexed]
    public string? Location { get; set; }
}

