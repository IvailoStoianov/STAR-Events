using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using STAREvents.Data.Models;

namespace STAREvents.Web.Infrastructure.Extensions
{
    public static class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager, IConfiguration configuration)
        {
            string? adminEmail = configuration["AdminCredentials:Email"];
            string? password = configuration["AdminCredentials:Password"];

            if (adminEmail == null || password == null)
            {
                throw new InvalidOperationException("Admin credentials are not configured properly.");
            }

            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid> { Name = "Admin" });
            }

            if (await roleManager.FindByNameAsync("User") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid> { Name = "User" });
            }

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                ApplicationUser admin = new ApplicationUser
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Email = adminEmail,
                    UserName = adminEmail,
                    FirstName = "Admin",
                    LastName = "Adminov"
                };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
