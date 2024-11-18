using System.ComponentModel.DataAnnotations;

namespace UserService.Models
{
    public class UserModel
    {
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Avatar { get; set; }
    }
    public class UserDetailMode : UserModel
    {
        public string? UserId { get; set; }
        public string? Role { get; set; }
    }
    public class PasswordModel
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        [Compare("NewPassword")]
        public string ConfirmPassword { get ; set; }
    }
}
