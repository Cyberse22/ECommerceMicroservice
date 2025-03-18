using System.ComponentModel.DataAnnotations;

namespace UserService.Models
{
    public class SignInModel
    {
        [Required]
        [Phone]
        public string? PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
