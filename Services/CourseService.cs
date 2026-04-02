using CourseManagementAPI.Data;
using CourseManagementAPI.DTOs.Course;
using CourseManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementAPI.Services
{
    public class CourseService
    {
        private readonly AppDbContext _context;

        public CourseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CourseReadDto>> GetAllAsync()
        {
            return await _context.Courses
                .AsNoTracking()
                .Include(c => c.Instructor)
                .Select(c => new CourseReadDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    InstructorId = c.InstructorId,
                    InstructorName = c.Instructor.Name
                })
                .ToListAsync();
        }

        public async Task<CourseReadDto?> GetByIdAsync(int id)
        {
            return await _context.Courses
                .AsNoTracking()
                .Include(c => c.Instructor)
                .Where(c => c.Id == id)
                .Select(c => new CourseReadDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    InstructorId = c.InstructorId,
                    InstructorName = c.Instructor.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task<CourseReadDto?> CreateAsync(CourseCreateDto dto)
        {
            var instructorExists = await _context.Instructors.AnyAsync(i => i.Id == dto.InstructorId);
            if (!instructorExists)
                return null;

            var course = new Course
            {
                Title = dto.Title,
                InstructorId = dto.InstructorId
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            var instructor = await _context.Instructors.FindAsync(dto.InstructorId);

            return new CourseReadDto
            {
                Id = course.Id,
                Title = course.Title,
                InstructorId = course.InstructorId,
                InstructorName = instructor!.Name
            };
        }

        public async Task<bool> UpdateAsync(int id, CourseUpdateDto dto)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return false;

            var instructorExists = await _context.Instructors.AnyAsync(i => i.Id == dto.InstructorId);
            if (!instructorExists)
                return false;

            course.Title = dto.Title;
            course.InstructorId = dto.InstructorId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return false;

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}