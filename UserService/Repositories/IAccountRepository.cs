using Microsoft.AspNetCore.Identity;
using UserService.Data;
using UserService.Models;

namespace UserService.Repositories
{
    public interface IAccountRepository
    {
        Task<SignInResult> SignInAsync(string phoneNumber, string password);
        Task<IdentityResult> SignUpAsync(SignUpModel model);
        Task<IdentityResult> CreateAdmin(SignUpModel admin);
        Task<bool> AssignRoleAsync(ApplicationUser user, string role);
        Task<bool> ChangePasswordAsync(string phoneNumber, string oldPassword, string newPassword);
        Task UpdateUserAsync(UserDbContext user);
        Task<ApplicationUser> GetCurrentUserAsync(string phoneNumber);
    }
}
