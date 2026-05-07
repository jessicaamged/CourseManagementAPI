using CourseManagementAPI.Data;
using CourseManagementAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        public UsersController(AppDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        [Authorize(Roles = "Admin,Instructor")]
        [HttpGet("students")]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _context.Users
                .Where(u => u.Role == "Student")
                .Select(u => new
                {
                    u.Id,
                    u.Name,
                    u.Email
                })
                .ToListAsync();

            return Ok(students);
        }

        [Authorize(Roles = "Admin,Instructor")]
        [HttpGet("instructors")]
        public async Task<IActionResult> GetInstructors()
        {
            var instructors = await _context.Users
                .Where(u => u.Role == "Instructor")
                .Select(u => new
                {
                    u.Id,
                    u.Name,
                    u.Email
                })
                .ToListAsync();

            return Ok(instructors);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("instructors")]
        public async Task<IActionResult> CreateInstructor(CreateInstructorDto dto)
        {
            var exists = await _context.Users.AnyAsync(u => u.Email == dto.Email);

            if (exists)
                return BadRequest(new { message = "Email is already registered." });

            var instructor = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Role = "Instructor"
            };

            instructor.PasswordHash = _passwordHasher.HashPassword(instructor, dto.Password);

            _context.Users.Add(instructor);
            await _context.SaveChangesAsync();

            _context.InstructorProfiles.Add(new InstructorProfile
            {
                UserId = instructor.Id,
                Bio = ""
            });

            await _context.SaveChangesAsync();

            return Ok(new
            {
                instructor.Id,
                instructor.Name,
                instructor.Email,
                instructor.Role
            });
        }
    }

    public class CreateInstructorDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}