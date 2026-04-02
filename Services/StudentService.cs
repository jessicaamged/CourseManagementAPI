using CourseManagementAPI.Data;
using CourseManagementAPI.DTOs.Student;
using CourseManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementAPI.Services
{
    public class StudentService
    {
        private readonly AppDbContext _context;

        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<StudentReadDto>> GetAllAsync()
        {
            return await _context.Students
                .AsNoTracking()
                .Select(s => new StudentReadDto
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .ToListAsync();
        }

        public async Task<StudentReadDto?> GetByIdAsync(int id)
        {
            return await _context.Students
                .AsNoTracking()
                .Where(s => s.Id == id)
                .Select(s => new StudentReadDto
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task<StudentReadDto> CreateAsync(StudentCreateDto dto)
        {
            var student = new Student
            {
                Name = dto.Name
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return new StudentReadDto
            {
                Id = student.Id,
                Name = student.Name
            };
        }

        public async Task<bool> UpdateAsync(int id, StudentUpdateDto dto)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return false;

            student.Name = dto.Name;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return false;

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}