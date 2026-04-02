using System.ComponentModel.DataAnnotations;

namespace CourseManagementAPI.DTOs.Course
{
    public class CourseUpdateDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(150)]
        public string Title { get; set; }

        [Range(1, int.MaxValue)]
        public int InstructorId { get; set; }
    }
}