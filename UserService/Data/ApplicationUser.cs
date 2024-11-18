using Microsoft.AspNetCore.Identity;

namespace UserService.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }
        public virtual ICollection<ApplicationUserRole>? UserRoles { get; set; }
    }
    public class ApplicationRole : IdentityRole { 
        public virtual ICollection<ApplicationUserRole>? UserRoles { get; set; }
    }

    public class ApplicationUserRole : IdentityUserRole<string> { 
       public virtual ApplicationUser? User { get; set; }
       public virtual ApplicationRole? Role { get; set; }
    }

}
