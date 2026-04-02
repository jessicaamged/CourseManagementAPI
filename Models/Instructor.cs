using System.Collections.Generic;

namespace CourseManagementAPI.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // One-to-One: Instructor ↔ InstructorProfile
        public InstructorProfile Profile { get; set; }

        // One-to-Many: Instructor → Courses
        public List<Course> Courses { get; set; } = new();
    }
}