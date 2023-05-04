using System.ComponentModel.DataAnnotations;

namespace ClassRoomMVC.Models
{
    public class CreateSchoolDto
    {
        [StringLength(50)]
        public string Name { get; set; }
        public string? Description { get; set; }
        public IFormFile Photo { get; set; }
    }
}
