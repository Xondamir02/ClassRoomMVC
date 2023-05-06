using System.ComponentModel.DataAnnotations;

namespace ClassRoomMVC.Models;

public class CreateScienceDto
{

    [StringLength(50)]
    public string Name { get; set; }
    public string? Description { get; set; }
}