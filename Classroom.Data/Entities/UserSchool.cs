using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classroom.Data.Entities
{
    public class UserSchool
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid SchoolId { get; set; }
        public School School { get; set; }
        public EUserSchool Type { get; set; }
    }
}
