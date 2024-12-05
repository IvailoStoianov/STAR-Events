using Microsoft.AspNetCore.Identity;
using STAREvents.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAREvents.Web.Infrastructure.Extensions
{
    public static class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            string adminEmail = "admin@example.com";
            string password = "Admin123!";
            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid> { Name = "Admin" });
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
