using Microsoft.AspNetCore.Identity;
using UserService.Data;
using UserService.Models;

namespace UserService.Repositories
{
    public interface IAccountRepository
    {
        Task<SignInResult> SignInAsync(string email, string password);
        Task<IdentityResult> SignUpAsync(SignUpModel model);
        Task<IdentityResult> CreateAdmin(CreateAdmin model);
        Task<bool> AssignRoleAsync(ApplicationUser user, string role);
        Task<bool> ChangePasswordAsync(string email, string oldPassword, string newPassword);
        Task UpdateUserAsync(UserDbContext user);
        Task<ApplicationUser> GetCurrentUserAsync(string email);
    }
}
