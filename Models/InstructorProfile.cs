namespace CourseManagementAPI.Models
{
    public class InstructorProfile
    {
        public int Id { get; set; }
        public string Bio { get; set; }

        // Foreign Key to Instructor
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }
    }
}