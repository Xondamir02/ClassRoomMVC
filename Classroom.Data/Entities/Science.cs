using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classroom.Data.Entities;

[Table("science")]
public class Science
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }

    public Guid SchoolId { get; set; }
    public School? School { get; set; }

    public List<UserScience>? UserSciences { get; set; }
}