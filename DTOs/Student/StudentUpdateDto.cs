using System.ComponentModel.DataAnnotations;

namespace CourseManagementAPI.DTOs.Student
{
    public class StudentUpdateDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}