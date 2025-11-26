using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thesis.Models
{
    public class FoodMeal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Type { get; set; } = "Lunch"; // Breakfast, Lunch, Dinner, Snack

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, 480)] // 1 minute to 8 hours
        public int PreparationTime { get; set; } // in minutes

        [Required]
        [Range(1, 1000)]
        public int Servings { get; set; }

        [MaxLength(50)]
        public string? ChefName { get; set; }

        public bool IsVegetarian { get; set; }

        [MaxLength(200)]
        public string? Allergens { get; set; }

        public int? FoodScheduleId { get; set; }

        // Navigation properties
        public FoodSchedule? FoodSchedule { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}