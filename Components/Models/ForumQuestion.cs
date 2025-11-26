using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Thesis.Components.Models;

namespace Thesis.Models
{
    public class ForumQuestion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        public string Tags { get; set; } = string.Empty; // Comma-separated tags

        [NotMapped]
        public List<string> TagsList => string.IsNullOrEmpty(Tags)
            ? new List<string>()
            : Tags.Split(',').Select(t => t.Trim()).Where(t => !string.IsNullOrEmpty(t)).ToList();

        public string? AttachmentPath { get; set; }

        public int ViewCount { get; set; } = 0;
        public int Likes { get; set; } = 0;
        public bool IsResolved { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        [ForeignKey(nameof(StudentId))]
        public User? Student { get; set; }

        public ICollection<ForumAnswer> Answers { get; set; } = new List<ForumAnswer>();
    }
}