using System.ComponentModel.DataAnnotations;

namespace Thesis.Models
{
    public class CafeteriaWorker
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }

        public bool IsActive { get; set; } = true;

        [MaxLength(50)]
        public string Role { get; set; } = "Volunteer";

        // Navigation properties
        public User? Student { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}