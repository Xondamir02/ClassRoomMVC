using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classroom.Data.Entities
{
    public class UserScience
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Guid ScienceId { get; set; }
        public Science? Science { get; set; }
        
    }
}
