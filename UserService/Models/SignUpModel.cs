using System.ComponentModel.DataAnnotations;

namespace UserService.Models
{
    public class SignUpModel
    {
        [Required]
        public string? FirstName {  get; set; }
        [Required]
        public string? LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Gender { get; set; }
        [Required, EmailAddress]
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required, Compare("Password")] public string? PasswordConfirmation { get; set; }
    }

    public class CreateAdmin : SignUpModel
    {
        public string Role { get; set; } = "Admin";
    }
}
