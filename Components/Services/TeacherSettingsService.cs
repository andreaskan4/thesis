using Microsoft.EntityFrameworkCore;
using Thesis.Data;
using Thesis.Models;

namespace Thesis.Services
{
    public class TeacherSettingsService
    {
        private readonly AppDbContext _context;

        public TeacherSettingsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TeacherSettings> GetTeacherSettingsAsync(int teacherId)
        {
            var settings = await _context.TeacherSettings
                .FirstOrDefaultAsync(ts => ts.TeacherId == teacherId);

            if (settings == null)
            {
                // Create default settings if none exist
                settings = new TeacherSettings
                {
                    TeacherId = teacherId,
                    DefaultGradingScale = "0-100",
                    DefaultExercisePoints = 100,
                    DefaultDueTime = "23:59",
                    MaxFileSizeMB = 10,
                    NotificationFrequency = "immediate",
                    Theme = "light",
                    Language = "en",
                    TimeZone = "UTC"
                };

                _context.TeacherSettings.Add(settings);
                await _context.SaveChangesAsync();
            }

            return settings;
        }

        public async Task<TeacherSettings> UpdateTeacherSettingsAsync(int teacherId, TeacherSettings newSettings)
        {
            var existingSettings = await _context.TeacherSettings
                .FirstOrDefaultAsync(ts => ts.TeacherId == teacherId);

            if (existingSettings != null)
            {
                // Update all properties
                existingSettings.DefaultGradingScale = newSettings.DefaultGradingScale;
                existingSettings.AutoPublishGrades = newSettings.AutoPublishGrades;
                existingSettings.GradeNotifications = newSettings.GradeNotifications;
                existingSettings.GradeRounding = newSettings.GradeRounding;
                existingSettings.AutoArchiveCourses = newSettings.AutoArchiveCourses;
                existingSettings.AllowStudentWithdrawals = newSettings.AllowStudentWithdrawals;
                existingSettings.DefaultCourseTemplate = newSettings.DefaultCourseTemplate;
                existingSettings.MaxStudentsPerCourse = newSettings.MaxStudentsPerCourse;
                existingSettings.DefaultExercisePoints = newSettings.DefaultExercisePoints;
                existingSettings.DefaultDueTime = newSettings.DefaultDueTime;
                existingSettings.AllowLateSubmissions = newSettings.AllowLateSubmissions;
                existingSettings.LateSubmissionPenalty = newSettings.LateSubmissionPenalty;
                existingSettings.RequireFileUpload = newSettings.RequireFileUpload;
                existingSettings.MaxFileSizeMB = newSettings.MaxFileSizeMB;
                existingSettings.NotifyNewSubmissions = newSettings.NotifyNewSubmissions;
                existingSettings.NotifyLateSubmissions = newSettings.NotifyLateSubmissions;
                existingSettings.NotifyGradeViews = newSettings.NotifyGradeViews;
                existingSettings.EmailDigest = newSettings.EmailDigest;
                existingSettings.NotificationFrequency = newSettings.NotificationFrequency;
                existingSettings.OfficeLocation = newSettings.OfficeLocation;
                existingSettings.OfficeHours = newSettings.OfficeHours;
                existingSettings.ContactPhone = newSettings.ContactPhone;
                existingSettings.Department = newSettings.Department;
                existingSettings.Bio = newSettings.Bio;
                existingSettings.Theme = newSettings.Theme;
                existingSettings.Language = newSettings.Language;
                existingSettings.EmailNotifications = newSettings.EmailNotifications;
                existingSettings.PushNotifications = newSettings.PushNotifications;
                existingSettings.TimeZone = newSettings.TimeZone;
                existingSettings.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();
                return existingSettings;
            }
            else
            {
                // Create new settings
                newSettings.TeacherId = teacherId;
                newSettings.CreatedAt = DateTime.Now;
                newSettings.UpdatedAt = DateTime.Now;
                _context.TeacherSettings.Add(newSettings);
                await _context.SaveChangesAsync();
                return newSettings;
            }
        }
    }
}