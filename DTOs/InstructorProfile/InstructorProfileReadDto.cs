namespace CourseManagementAPI.DTOs.InstructorProfile
{
    public class InstructorProfileReadDto
    {
        public int Id { get; set; }
        public string Bio { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string InstructorName { get; set; } = string.Empty;
    }
}