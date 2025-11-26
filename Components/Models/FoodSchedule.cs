using System.ComponentModel.DataAnnotations;

namespace Thesis.Models
{
    public class FoodSchedule
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Day { get; set; }

        // Navigation properties
        public List<FoodMeal> Meals { get; set; } = new();

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}