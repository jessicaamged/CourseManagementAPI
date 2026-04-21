using System.Collections.Generic;

namespace CourseManagementAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        // Admin / Instructor / Student
        public string Role { get; set; } = string.Empty;

        // Only for instructors
        public InstructorProfile? InstructorProfile { get; set; }

        // If the user is an instructor, these are the courses they teach
        public List<Course> CoursesTeaching { get; set; } = new();

        // If the user is a student, these are their enrollments
        public List<Enrollment> Enrollments { get; set; } = new();
    }
}