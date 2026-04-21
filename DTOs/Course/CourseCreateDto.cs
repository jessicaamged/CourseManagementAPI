
namespace CourseManagementAPI.DTOs.Course
{
    public class CourseCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int InstructorId { get; set; }
    }
}