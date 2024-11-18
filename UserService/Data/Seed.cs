using Microsoft.AspNetCore.Identity;
using UserService.Helpers;

namespace UserService.Data
{
    public class Seed
    {
        public static async Task SeedDataAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { StaticEntities.UserRoles.Admin, StaticEntities.UserRoles.Staff, StaticEntities.UserRoles.Customer };
            foreach (var role in roles) 
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            var adminEmail = "admin@gmail.com";
            var adminUser = await userManager.FindByNameAsync(adminEmail);
            if (adminUser == null) 
            {
                var newAdmin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Admin",
                    LastName = "Admin",
                    EmailConfirmed = true,
                };
                var result = await userManager.CreateAsync(newAdmin, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdmin, StaticEntities.UserRoles.Admin);
                }
            }
        }
    }
}
