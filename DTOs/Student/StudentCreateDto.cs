using System.ComponentModel.DataAnnotations;

namespace CourseManagementAPI.DTOs.Student
{
    public class StudentCreateDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}