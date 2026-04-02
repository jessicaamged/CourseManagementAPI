using System.Collections.Generic;

namespace CourseManagementAPI.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }

        // Foreign Key to Instructor (1-M)
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }

        // Many-to-Many: Course ↔ Student
        public List<Enrollment> Enrollments { get; set; } = new();
    }
}