using System.ComponentModel.DataAnnotations;

namespace CourseManagementAPI.DTOs.Auth
{
    public class LoginDto
    {
        [Required]
        [MinLength(3)]
        public string Username { get; set; }

        [Required]
        [MinLength(4)]
        public string Password { get; set; }
    }
}