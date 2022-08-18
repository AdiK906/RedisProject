using Domain.Entities;
using Redis.OM;
using Redis.OM.Searching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisProject.UnitTest.Commands.AddStudentCommand
{
    public class AddStudentCommandTest
    {
        private readonly RedisCollection<Student> _student;

        public AddStudentCommandTest(RedisConnectionProvider provider)
        {
            _student = (RedisCollection<Student>)provider.RedisCollection<Student>();
        }
        public void AddStudentToRedis()
        {
            var student = new Student { FirstMidName = "Tescik", LastName = "Testowyyyyy" }; //11:46
            
            //AddStudentCommand()
        }
    }
}
