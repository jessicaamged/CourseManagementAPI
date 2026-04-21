using CourseManagementAPI.DTOs.Enrollment;
using CourseManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EnrollmentsController : ControllerBase
    {
        private readonly EnrollmentService _enrollmentService;

        public EnrollmentsController(EnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [Authorize(Roles = "Admin,Instructor")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var enrollments = await _enrollmentService.GetAllAsync();
            return Ok(enrollments);
        }

        [Authorize(Roles = "Student,Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(EnrollmentCreateDto dto)
        {
            var createdEnrollment = await _enrollmentService.CreateAsync(dto);
            if (createdEnrollment == null)
                return BadRequest(new { message = "Invalid student/course or enrollment already exists." });

            return Ok(createdEnrollment);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{userId}/{courseId}")]
        public async Task<IActionResult> Delete(int userId, int courseId)
        {
            var deleted = await _enrollmentService.DeleteAsync(userId, courseId);
            if (!deleted)
                return NotFound(new { message = "Enrollment not found." });

            return NoContent();
        }
    }
}