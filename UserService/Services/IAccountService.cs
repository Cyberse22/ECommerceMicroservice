using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;
using UserService.Data;
using UserService.Models;

namespace UserService.Services
{
    public interface IAccountService
    {
        Task<IdentityResult> SignUpAsync(SignUpModel model);
        Task<IdentityResult> CreateAdminAsync(SignUpModel admin);
        Task<string> SignInAsync(SignInModel model);
        Task<bool> AssignRoleAsync(ApplicationUser user, string role);
        Task<bool> ChangePasswordAsync(string phoneNumber, string currentPassword, string newPassword);
        Task<UserModel> GetCurrentUserAsync();
    }
}
