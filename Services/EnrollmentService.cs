using CourseManagementAPI.Data;
using CourseManagementAPI.DTOs.Enrollment;
using CourseManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementAPI.Services
{
    public class EnrollmentService
    {
        private readonly AppDbContext _context;

        public EnrollmentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EnrollmentReadDto>> GetAllAsync()
        {
            return await _context.Enrollments
                .AsNoTracking()
                .Include(e => e.Student)
                .Include(e => e.Course)
                .Select(e => new EnrollmentReadDto
                {
                    StudentId = e.StudentId,
                    StudentName = e.Student.Name,
                    CourseId = e.CourseId,
                    CourseTitle = e.Course.Title
                })
                .ToListAsync();
        }

        public async Task<EnrollmentReadDto?> CreateAsync(EnrollmentCreateDto dto)
        {
            var studentExists = await _context.Students.AnyAsync(s => s.Id == dto.StudentId);
            var courseExists = await _context.Courses.AnyAsync(c => c.Id == dto.CourseId);

            if (!studentExists || !courseExists)
                return null;

            var alreadyExists = await _context.Enrollments.AnyAsync(e =>
                e.StudentId == dto.StudentId && e.CourseId == dto.CourseId);

            if (alreadyExists)
                return null;

            var enrollment = new Enrollment
            {
                StudentId = dto.StudentId,
                CourseId = dto.CourseId
            };

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            var student = await _context.Students.FindAsync(dto.StudentId);
            var course = await _context.Courses.FindAsync(dto.CourseId);

            return new EnrollmentReadDto
            {
                StudentId = dto.StudentId,
                StudentName = student!.Name,
                CourseId = dto.CourseId,
                CourseTitle = course!.Title
            };
        }

        public async Task<bool> DeleteAsync(int studentId, int courseId)
        {
            var enrollment = await _context.Enrollments.FindAsync(studentId, courseId);
            if (enrollment == null)
                return false;

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}