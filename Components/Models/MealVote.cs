using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thesis.Models
{
    public class MealVote
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int MealId { get; set; }

        [Required]
        public DateTime VoteDate { get; set; }

        // Navigation properties
        public User? Student { get; set; }
        public FoodMeal? Meal { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}