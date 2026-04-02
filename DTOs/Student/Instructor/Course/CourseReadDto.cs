namespace CourseManagementAPI.DTOs.Course
{
    public class CourseReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int InstructorId { get; set; }
        public string InstructorName { get; set; }
    }
}