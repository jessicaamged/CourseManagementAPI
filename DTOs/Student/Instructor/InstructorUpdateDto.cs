using System.ComponentModel.DataAnnotations;

namespace CourseManagementAPI.DTOs.Instructor
{
    public class InstructorUpdateDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(500)]
        public string Bio { get; set; }
    }
}