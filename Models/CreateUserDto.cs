using System.ComponentModel.DataAnnotations;

namespace ClassRoomMVC.Models
{
    public class CreateUserDto
    {
        [StringLength(50), MinLength(3)]
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        [RegularExpression("^[0-9]{9}$",ErrorMessage = "Phone number is not valid")]
        public string PhoneNumber { get;set; }

        [StringLength(50), MinLength(3)]
        public string Username { get; set; }
        [StringLength(50),MinLength(6)]
        public string Password { get; set; }


        [Required]
        public IFormFile? Photo { get; set; }
    }
}
