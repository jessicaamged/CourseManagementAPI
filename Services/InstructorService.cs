using CourseManagementAPI.Data;
using CourseManagementAPI.DTOs.Instructor;
using CourseManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementAPI.Services
{
    public class InstructorService
    {
        private readonly AppDbContext _context;

        public InstructorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<InstructorReadDto>> GetAllAsync()
        {
            return await _context.Instructors
                .AsNoTracking()
                .Include(i => i.Profile)
                .Select(i => new InstructorReadDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    Bio = i.Profile != null ? i.Profile.Bio : ""
                })
                .ToListAsync();
        }

        public async Task<InstructorReadDto?> GetByIdAsync(int id)
        {
            return await _context.Instructors
                .AsNoTracking()
                .Include(i => i.Profile)
                .Where(i => i.Id == id)
                .Select(i => new InstructorReadDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    Bio = i.Profile != null ? i.Profile.Bio : ""
                })
                .FirstOrDefaultAsync();
        }

        public async Task<InstructorReadDto> CreateAsync(InstructorCreateDto dto)
        {
            var instructor = new Instructor
            {
                Name = dto.Name,
                Profile = new InstructorProfile
                {
                    Bio = dto.Bio
                }
            };

            _context.Instructors.Add(instructor);
            await _context.SaveChangesAsync();

            return new InstructorReadDto
            {
                Id = instructor.Id,
                Name = instructor.Name,
                Bio = instructor.Profile.Bio
            };
        }

        public async Task<bool> UpdateAsync(int id, InstructorUpdateDto dto)
        {
            var instructor = await _context.Instructors
                .Include(i => i.Profile)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (instructor == null)
                return false;

            instructor.Name = dto.Name;

            if (instructor.Profile == null)
            {
                instructor.Profile = new InstructorProfile
                {
                    Bio = dto.Bio,
                    InstructorId = instructor.Id
                };
            }
            else
            {
                instructor.Profile.Bio = dto.Bio;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var instructor = await _context.Instructors
                .Include(i => i.Profile)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (instructor == null)
                return false;

            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}