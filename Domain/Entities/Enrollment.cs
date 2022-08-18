using Redis.OM.Modeling;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
[Document(StorageType = StorageType.Json, Prefixes = new[] { "Enrollment" })]
public class Enrollment
{
    [Indexed] [RedisIdField]
    public string? Id { get; set; }
    public string? Grade { get; set; }

}
