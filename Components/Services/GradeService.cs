using Microsoft.EntityFrameworkCore;
using Thesis.Data;
using Thesis.Models;

namespace Thesis.Services;

public class GradesService
{
    private readonly AppDbContext _context;

    public GradesService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Grade>> GetAllGradesAsync()
    {
        return await _context.Grades
            .Include(g => g.Student)
            .Include(g => g.Course)
            .ToListAsync();
    }

    public async Task<List<User>> GetStudentsAsync()
    {
        return await _context.Users.Where(u => u.Role == "Student").ToListAsync();
    }

    public async Task<List<User>> GetProfessorsAsync()
    {
        return await _context.Users.Where(u => u.Role == "Teacher").ToListAsync();
    }

    public async Task<List<Course>> GetCoursesAsync()
    {
        return await _context.Courses.ToListAsync();
    }

    public async Task AddGradeAsync(Grade grade)
    {
        _context.Grades.Add(grade);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateGradeAsync(Grade grade)
    {
        var existing = await _context.Grades.FindAsync(grade.Id);
        if (existing != null)
        {
            existing.StudentId = grade.StudentId;
            existing.CourseId = grade.CourseId;
            existing.GradeValue = grade.GradeValue;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteGradeAsync(int id)
    {
        var grade = await _context.Grades.FindAsync(id);
        if (grade != null)
        {
            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();
        }
    }
    public async Task AddCourseAsync(Course course)
    {
        // Ensure GradingMethod is null for new courses
        course.GradingMethod = null;
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCourseAsync(Course course)
    {
        var existing = await _context.Courses.FindAsync(course.Id);
        if (existing != null)
        {
            existing.Name = course.Name;
            existing.Description = course.Description;
            existing.ProfessorId = course.ProfessorId;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteCourseAsync(int id)
    {
        try
        {
            Console.WriteLine($"Attempting to delete course with ID: {id}");

            // First, check if the course exists
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                Console.WriteLine("Course not found");
                return;
            }

            // Check for related enrollments
            var relatedEnrollments = await _context.Enrollments.Where(e => e.CourseId == id).ToListAsync();
            Console.WriteLine($"Found {relatedEnrollments.Count} enrollments linked to this course");

            // Check for related grades
            var relatedGrades = await _context.Grades.Where(g => g.CourseId == id).ToListAsync();
            Console.WriteLine($"Found {relatedGrades.Count} grades linked to this course");

            // Check for related schedules  
            var relatedSchedules = await _context.Schedules.Where(s => s.CourseId == id).ToListAsync();
            Console.WriteLine($"Found {relatedSchedules.Count} schedules linked to this course");

            // Delete related enrollments first
            if (relatedEnrollments.Any())
            {
                Console.WriteLine("Deleting related enrollments...");
                _context.Enrollments.RemoveRange(relatedEnrollments);
                await _context.SaveChangesAsync();
            }

            // Delete related grades
            if (relatedGrades.Any())
            {
                Console.WriteLine("Deleting related grades...");
                _context.Grades.RemoveRange(relatedGrades);
                await _context.SaveChangesAsync();
            }

            // Delete related schedules
            if (relatedSchedules.Any())
            {
                Console.WriteLine("Deleting related schedules...");
                _context.Schedules.RemoveRange(relatedSchedules);
                await _context.SaveChangesAsync();
            }

            // Now delete the course
            Console.WriteLine("Deleting course...");
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            Console.WriteLine("Course deleted successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR in DeleteCourseAsync: {ex.Message}");
            Console.WriteLine($"Inner exception: {ex.InnerException?.Message}");
            throw;
        }
    }

    public async Task<List<Schedule>> GetSchedulesAsync()
    {
        return await _context.Schedules.ToListAsync();
    }

    public async Task AddScheduleAsync(Schedule schedule)
    {
        _context.Schedules.Add(schedule);
        await _context.SaveChangesAsync();
    }
    public async Task<List<Enrollment>> GetEnrollmentsForCourseAsync(int courseId)
    {
        return await _context.Enrollments
            .Where(e => e.CourseId == courseId)
            .Include(e => e.Student)
            .ToListAsync();
    }

    public async Task DeleteScheduleAsync(int id)
    {
        var schedule = await _context.Schedules.FindAsync(id);
        if (schedule != null)
        {
            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();
        }
    }
}