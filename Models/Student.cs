using System.Collections.Generic;

namespace CourseManagementAPI.Models
{
    public class Student
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        // Many-to-Many: Student ↔ Course
        public List<Enrollment> Enrollments { get; set; } = new();
    }
}