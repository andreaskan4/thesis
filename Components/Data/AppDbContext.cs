using Microsoft.EntityFrameworkCore;
using Thesis.Models;
using Thesis.Components.Models; // ✅ cleaner import

namespace Thesis.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<ContactInfo> ContactInfo { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageReply> MessageReplies { get; set; }
        public DbSet<ProfessorProfileEntity> ProfessorProfiles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ProfessorRating> ProfessorRatings { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<CourseRating> CourseRatings { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseSubmission> ExerciseSubmissions { get; set; }

        // ✅ Rename to match the actual table name
        public DbSet<AdminSystemSettings> AdminSystemSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✅ Ensure EF maps to the correct SQLite table
            modelBuilder.Entity<AdminSystemSettings>().ToTable("AdminSystemSettings");

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Role).IsRequired().HasMaxLength(20);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("datetime('now')");
                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.GradeValue).HasColumnName("Grade");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Student)
                      .WithMany()
                      .HasForeignKey(e => e.StudentId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Course)
                      .WithMany()
                      .HasForeignKey(e => e.CourseId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasIndex(e => new { e.StudentId, e.CourseId }).IsUnique();
            });
            // Configure ProfessorProfileEntity for SQLite
            modelBuilder.Entity<ProfessorProfileEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("datetime('now')");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("datetime('now')");

                // Ensure each user has only one profile
                entity.HasIndex(e => e.UserId).IsUnique();
            });
            // ✅ Configure Facility entity for SQLite
            modelBuilder.Entity<Facility>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Type).IsRequired().HasMaxLength(100);
                entity.Property(e => e.BuildingName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Address).IsRequired().HasMaxLength(500);
                entity.Property(e => e.PhotosJson).HasDefaultValue("[]");
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("datetime('now')");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("datetime('now')");
            });

            // ✅ Configure ContactInfo entity for SQLite
            modelBuilder.Entity<ContactInfo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.MainAddress).IsRequired().HasMaxLength(500);
                entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
                entity.Property(e => e.SecretaryName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Facebook).HasMaxLength(500);
                entity.Property(e => e.Twitter).HasMaxLength(500);
                entity.Property(e => e.Instagram).HasMaxLength(500);
                entity.Property(e => e.LinkedIn).HasMaxLength(500);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("datetime('now')");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("datetime('now')");
            });
            modelBuilder.Entity<Message>()
                .HasMany(m => m.Replies)
                .WithOne(r => r.ParentMessage)
                .HasForeignKey(r => r.MessageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}