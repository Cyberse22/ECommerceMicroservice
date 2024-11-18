using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using UserService.Data;
using UserService.Helpers;
using UserService.Models;

namespace UserService.Repositories.Impl
{
    public class AccountRepositoryImpl : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserDbContext _userDbContext;

        public AccountRepositoryImpl(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, UserDbContext userDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userDbContext = userDbContext; 
        }

        public async Task<bool> AssignRoleAsync(ApplicationUser user, string role)
        {
            var roleExists = await _roleManager.RoleExistsAsync(role);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
            var result = await _userManager.AddToRoleAsync(user, role);
            return result.Succeeded;
        }

        public async Task<bool> ChangePasswordAsync(string email, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            { 
                var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
                return result.Succeeded;
            }
            return false;
        }

        public async Task<IdentityResult> CreateAdmin(CreateAdmin model)
        {
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                PhoneNumber = model.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(StaticEntities.UserRoles.Admin))
                {
                    await _roleManager.CreateAsync(new IdentityRole(StaticEntities.UserRoles.Admin));
                }
                await _userManager.AddToRoleAsync(user, StaticEntities.UserRoles.Admin);
            }
            return result;
        }

        public async Task<ApplicationUser> GetCurrentUserAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<SignInResult> SignInAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) 
            { 
               return SignInResult.Failed; 
            }
            var passwordValid = await _userManager.CheckPasswordAsync(user, password);
            if (!passwordValid)
            {
                return SignInResult.Failed;
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>();
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }
            return await _signInManager.PasswordSignInAsync(email, password, false, false);
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                PhoneNumber = model.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded) 
            {
                if(!await _roleManager.RoleExistsAsync(StaticEntities.UserRoles.Customer))
                {
                    await _roleManager.CreateAsync(new IdentityRole(StaticEntities.UserRoles.Customer));
                }
                await _userManager.AddToRoleAsync(user, StaticEntities.UserRoles.Customer);
            }
            return result;
        }

        public async Task UpdateUserAsync(UserDbContext user)
        {
            _userDbContext.Entry(user).State = EntityState.Modified;
            await _userDbContext.SaveChangesAsync();
        }
    }
}
