using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rent.DomainModels.Models
{
    public class RentDbInitializer
    {
        public static async Task SeedData(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                AppDbContext context = serviceScope
                    .ServiceProvider
                    .GetRequiredService<AppDbContext>();

                UserManager<User> userManager = serviceScope
                    .ServiceProvider
                    .GetRequiredService<UserManager<User>>();

                RoleManager<IdentityRole> roleManager = serviceScope
                    .ServiceProvider
                    .GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
                }

                var user = await userManager.FindByNameAsync("admin@rent.com");
                if (user == null)
                {
                    user = new User
                    {
                        UserName = "admin@rent.com",
                        Email = "admin@rent.com"
                    };
                    await userManager.CreateAsync(user, "Password!21092");
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
