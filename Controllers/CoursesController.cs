using CourseManagementAPI.DTOs.Course;
using CourseManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateDto dto)
        {
            var createdCourse = await _courseService.CreateAsync(dto);
            if (createdCourse == null)
                return BadRequest(new { message = "Instructor does not exist." });

            return CreatedAtAction(nameof(GetById), new { id = createdCourse.Id }, createdCourse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CourseUpdateDto dto)
        {
            var updated = await _courseService.UpdateAsync(id, dto);
            if (!updated)
                return BadRequest(new { message = "Course not found or instructor does not exist." });

            return NoContent();
        }

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