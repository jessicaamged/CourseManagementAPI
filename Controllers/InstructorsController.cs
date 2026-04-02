using CourseManagementAPI.DTOs.Instructor;
using CourseManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstructorsController : ControllerBase
    {
        private readonly InstructorService _instructorService;

        public InstructorsController(InstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var instructors = await _instructorService.GetAllAsync();
            return Ok(instructors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var instructor = await _instructorService.GetByIdAsync(id);
            if (instructor == null)
                return NotFound(new { message = "Instructor not found." });

            return Ok(instructor);
        }

        [HttpPost]
        public async Task<IActionResult> Create(InstructorCreateDto dto)
        {
            var createdInstructor = await _instructorService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdInstructor.Id }, createdInstructor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, InstructorUpdateDto dto)
        {
            var updated = await _instructorService.UpdateAsync(id, dto);
            if (!updated)
                return NotFound(new { message = "Instructor not found." });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _instructorService.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { message = "Instructor not found." });

            return NoContent();
        }
    }
}