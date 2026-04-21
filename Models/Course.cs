using System.Collections.Generic;

namespace CourseManagementAPI.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        // Foreign key to User who is the instructor
        public int InstructorId { get; set; }
        public User? Instructor { get; set; }

        public List<Enrollment> Enrollments { get; set; } = new();
    }
}