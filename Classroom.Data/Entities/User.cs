using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Classroom.Data.Entities
{
    public class User:IdentityUser<Guid>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime Data { get; set; }
        public List<UserSchool> UserSchools { get; set; }
        public List<UserScience>? UserSciences { get; set; }
    }
}
