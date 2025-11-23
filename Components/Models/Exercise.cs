// Add to your existing Models folder
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thesis.Models
{
    public class Exercise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public string Instructions { get; set; } = string.Empty;

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        [Range(1, 1000)]
        public double MaxPoints { get; set; }

        [Required]
        public bool IsPublished { get; set; } = false;

        public string? AttachmentPath { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        [ForeignKey(nameof(CourseId))]
        public Course? Course { get; set; }

        public ICollection<ExerciseSubmission> Submissions { get; set; } = new List<ExerciseSubmission>();
    }

    public class ExerciseSubmission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ExerciseId { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public string Answer { get; set; } = string.Empty;

        public string? FilePath { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.Now;

        public double? Grade { get; set; }

        public DateTime? GradedAt { get; set; }

        // Navigation properties
        [ForeignKey(nameof(ExerciseId))]
        public Exercise? Exercise { get; set; }

        [ForeignKey(nameof(StudentId))]
        public User? Student { get; set; }
    }
}