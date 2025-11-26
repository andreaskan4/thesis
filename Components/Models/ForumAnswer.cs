using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thesis.Models
{
    public class ForumAnswer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int QuestionId { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        public int Likes { get; set; } = 0;
        public bool IsAccepted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        [ForeignKey(nameof(QuestionId))]
        public ForumQuestion? Question { get; set; }

        [ForeignKey(nameof(StudentId))]
        public User? Student { get; set; }
    }
}