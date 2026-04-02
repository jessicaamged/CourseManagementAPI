using CourseManagementAPI.DTOs.Enrollment;
using CourseManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly EnrollmentService _enrollmentService;

        public EnrollmentsController(EnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var enrollments = await _enrollmentService.GetAllAsync();
            return Ok(enrollments);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EnrollmentCreateDto dto)
        {
            var createdEnrollment = await _enrollmentService.CreateAsync(dto);
            if (createdEnrollment == null)
                return BadRequest(new { message = "Invalid student/course or enrollment already exists." });

            return Ok(createdEnrollment);
        }

        [HttpDelete("{studentId}/{courseId}")]
        public async Task<IActionResult> Delete(int studentId, int courseId)
        {
            var deleted = await _enrollmentService.DeleteAsync(studentId, courseId);
            if (!deleted)
                return NotFound(new { message = "Enrollment not found." });

            return NoContent();
        }
    }
}