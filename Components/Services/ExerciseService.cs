using Microsoft.EntityFrameworkCore;
using Thesis.Data;
using Thesis.Models;

namespace Thesis.Services
{
    public class ExercisesService
    {
        private readonly AppDbContext _context;

        public ExercisesService(AppDbContext context)
        {
            _context = context;
        }

        // Exercise methods
        public async Task<List<Exercise>> GetExercisesByCourseAsync(int courseId)
        {
            return await _context.Exercises
                .Where(e => e.CourseId == courseId)
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Exercise>> GetExercisesByProfessorAsync(int professorId)
        {
            return await _context.Exercises
                .Include(e => e.Course)
                .Where(e => e.Course != null && e.Course.ProfessorId == professorId)
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync();
        }

        public async Task<Exercise?> GetExerciseByIdAsync(int id)
        {
            return await _context.Exercises
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Exercise> AddExerciseAsync(Exercise exercise)
        {
            exercise.CreatedAt = DateTime.Now;
            exercise.UpdatedAt = DateTime.Now;

            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync();
            return exercise;
        }

        public async Task<Exercise> UpdateExerciseAsync(Exercise exercise)
        {
            var existing = await _context.Exercises.FindAsync(exercise.Id);
            if (existing != null)
            {
                existing.Title = exercise.Title;
                existing.Description = exercise.Description;
                existing.Instructions = exercise.Instructions;
                existing.DueDate = exercise.DueDate;
                existing.MaxPoints = exercise.MaxPoints;
                existing.IsPublished = exercise.IsPublished;
                existing.AttachmentPath = exercise.AttachmentPath;
                existing.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();
            }
            return existing;
        }

        public async Task DeleteExerciseAsync(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise != null)
            {
                // Delete related submissions first
                var submissions = await _context.ExerciseSubmissions
                    .Where(s => s.ExerciseId == id)
                    .ToListAsync();

                _context.ExerciseSubmissions.RemoveRange(submissions);
                _context.Exercises.Remove(exercise);
                await _context.SaveChangesAsync();
            }
        }

        // Submission methods
        public async Task<List<ExerciseSubmission>> GetSubmissionsByExerciseAsync(int exerciseId)
        {
            return await _context.ExerciseSubmissions
                .Include(s => s.Student)
                .Where(s => s.ExerciseId == exerciseId)
                .OrderByDescending(s => s.SubmittedAt)
                .ToListAsync();
        }

        public async Task<ExerciseSubmission?> GetSubmissionByIdAsync(int id)
        {
            return await _context.ExerciseSubmissions
                .Include(s => s.Student)
                .Include(s => s.Exercise)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<ExerciseSubmission> AddSubmissionAsync(ExerciseSubmission submission)
        {
            submission.SubmittedAt = DateTime.Now;

            _context.ExerciseSubmissions.Add(submission);
            await _context.SaveChangesAsync();
            return submission;
        }

        public async Task<ExerciseSubmission> UpdateSubmissionGradeAsync(int submissionId, double grade)
        {
            var submission = await _context.ExerciseSubmissions.FindAsync(submissionId);
            if (submission != null)
            {
                submission.Grade = grade;
                submission.GradedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            return submission;
        }

        public async Task<int> GetSubmissionCountAsync(int exerciseId)
        {
            return await _context.ExerciseSubmissions
                .Where(s => s.ExerciseId == exerciseId)
                .CountAsync();
        }

        public async Task<double> GetAverageGradeAsync(int exerciseId)
        {
            var submissions = await _context.ExerciseSubmissions
                .Where(s => s.ExerciseId == exerciseId && s.Grade.HasValue)
                .ToListAsync();

            return submissions.Any() ? submissions.Average(s => s.Grade.Value) : 0;
        }

        public async Task<int> GetTotalExercisesCountAsync(int professorId)
        {
            return await _context.Exercises
                .Include(e => e.Course)
                .Where(e => e.Course != null && e.Course.ProfessorId == professorId)
                .CountAsync();
        }

        public async Task<int> GetTotalSubmissionsCountAsync(int professorId)
        {
            return await _context.ExerciseSubmissions
                .Include(s => s.Exercise)
                .ThenInclude(e => e.Course)
                .Where(s => s.Exercise != null && s.Exercise.Course != null && s.Exercise.Course.ProfessorId == professorId)
                .CountAsync();
        }
        // Add these methods to your existing ExercisesService class
        public async Task<List<ExerciseSubmission>> GetSubmissionsByStudentAsync(int studentId)
        {
            return await _context.ExerciseSubmissions
                .Include(s => s.Exercise)
                .ThenInclude(e => e.Course)
                .Where(s => s.StudentId == studentId)
                .OrderByDescending(s => s.SubmittedAt)
                .ToListAsync();
        }

        public async Task<ExerciseSubmission> UpdateSubmissionAsync(ExerciseSubmission submission)
        {
            var existing = await _context.ExerciseSubmissions.FindAsync(submission.Id);
            if (existing != null)
            {
                existing.Answer = submission.Answer;
                existing.FilePath = submission.FilePath;
                existing.SubmittedAt = DateTime.Now;
                // Don't update Grade and GradedAt - those are for professors only

                await _context.SaveChangesAsync();
            }
            return existing;
        }

        public async Task<ExerciseSubmission?> GetSubmissionByStudentAndExerciseAsync(int studentId, int exerciseId)
        {
            return await _context.ExerciseSubmissions
                .Include(s => s.Exercise)
                .FirstOrDefaultAsync(s => s.StudentId == studentId && s.ExerciseId == exerciseId);
        }
    }
}