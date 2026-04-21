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
                .Include(e => e.User)
                .Include(e => e.Course)
                .Select(e => new EnrollmentReadDto
                {
                    UserId = e.UserId,
                    UserName = e.User != null ? e.User.Name : "",
                    CourseId = e.CourseId,
                    CourseTitle = e.Course != null ? e.Course.Title : ""
                })
                .ToListAsync();
        }

        public async Task<EnrollmentReadDto?> CreateAsync(EnrollmentCreateDto dto)
        {
            var student = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == dto.UserId && u.Role == "Student");

            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == dto.CourseId);

            if (student == null || course == null)
                return null;

            var alreadyExists = await _context.Enrollments.AnyAsync(e =>
                e.UserId == dto.UserId && e.CourseId == dto.CourseId);

            if (alreadyExists)
                return null;

            var enrollment = new Enrollment
            {
                UserId = dto.UserId,
                CourseId = dto.CourseId
            };

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return new EnrollmentReadDto
            {
                UserId = dto.UserId,
                UserName = student.Name,
                CourseId = dto.CourseId,
                CourseTitle = course.Title
            };
        }

        public async Task<bool> DeleteAsync(int userId, int courseId)
        {
            var enrollment = await _context.Enrollments.FindAsync(userId, courseId);
            if (enrollment == null)
                return false;

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}