using CourseManagementAPI.DTOs.Course;
using CourseManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CoursesController : ControllerBase
    {
        private readonly CourseService _courseService;

        public CoursesController(CourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _courseService.GetAllAsync();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var course = await _courseService.GetByIdAsync(id);
            if (course == null)
                return NotFound(new { message = "Course not found." });

            return Ok(course);
        }

        [Authorize(Roles = "Instructor,Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateDto dto)
        {
            var createdCourse = await _courseService.CreateAsync(dto);
            if (createdCourse == null)
                return BadRequest(new { message = "Instructor does not exist or is not an instructor." });

            return CreatedAtAction(nameof(GetById), new { id = createdCourse.Id }, createdCourse);
        }

        [Authorize(Roles = "Instructor,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CourseUpdateDto dto)
        {
            var updated = await _courseService.UpdateAsync(id, dto);
            if (!updated)
                return BadRequest(new { message = "Course not found or instructor is invalid." });

            return NoContent();
        }

        [Authorize(Roles = "Instructor,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _courseService.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { message = "Course not found." });

            return NoContent();
        }
    }
}