using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thesis.Models
{
    public class TeacherSettings
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int TeacherId { get; set; }

        // Grading Settings
        public string DefaultGradingScale { get; set; } = "0-100";
        public bool AutoPublishGrades { get; set; } = true;
        public bool GradeNotifications { get; set; } = true;
        public string GradeRounding { get; set; } = "none";

        // Course Settings
        public bool AutoArchiveCourses { get; set; } = true;
        public bool AllowStudentWithdrawals { get; set; } = true;
        public string DefaultCourseTemplate { get; set; } = "standard";
        public int MaxStudentsPerCourse { get; set; } = 50;

        // Exercise Settings
        public int DefaultExercisePoints { get; set; } = 100;
        public string DefaultDueTime { get; set; } = "23:59";
        public bool AllowLateSubmissions { get; set; } = false;
        public int LateSubmissionPenalty { get; set; } = 10;
        public bool RequireFileUpload { get; set; } = false;
        public int MaxFileSizeMB { get; set; } = 10;

        // Notification Settings
        public bool NotifyNewSubmissions { get; set; } = true;
        public bool NotifyLateSubmissions { get; set; } = true;
        public bool NotifyGradeViews { get; set; } = false;
        public bool EmailDigest { get; set; } = true;
        public string NotificationFrequency { get; set; } = "immediate";

        // Profile Settings
        public string? OfficeLocation { get; set; }
        public string? OfficeHours { get; set; }
        public string? ContactPhone { get; set; }
        public string? Department { get; set; }
        public string? Bio { get; set; }

        // Personal Preferences
        public string Theme { get; set; } = "light";
        public string Language { get; set; } = "en";
        public bool EmailNotifications { get; set; } = true;
        public bool PushNotifications { get; set; } = true;
        public string TimeZone { get; set; } = "UTC";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [ForeignKey(nameof(TeacherId))]
        public User? Teacher { get; set; }
    }
}