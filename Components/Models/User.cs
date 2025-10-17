using System.ComponentModel.DataAnnotations;

namespace Thesis.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string Role { get; set; } = "Teacher";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}