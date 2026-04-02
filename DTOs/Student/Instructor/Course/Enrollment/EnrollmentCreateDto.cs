using System.ComponentModel.DataAnnotations;

namespace CourseManagementAPI.DTOs.Enrollment
{
    public class EnrollmentCreateDto
    {
        [Range(1, int.MaxValue)]
        public int StudentId { get; set; }

        [Range(1, int.MaxValue)]
        public int CourseId { get; set; }
    }
}