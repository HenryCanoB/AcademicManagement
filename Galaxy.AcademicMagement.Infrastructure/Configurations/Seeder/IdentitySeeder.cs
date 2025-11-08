using Galaxy.AcademicMagement.Infrastructure.Configurations.Entities.IdentityContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Galaxy.AcademicMagement.Infrastructure.Configurations.Seeder
{
    public class IdentitySeeder
    {
        public static async Task SeedAsync(IServiceProvider service)
        {
            // Implement data seeding logic for Identity context here.
            // This could involve creating default users, roles, etc.
            var userManager = service.GetRequiredService<UserManager<UserExtIdentity>>();
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

            // Example: Create default roles
            var manager = new IdentityRole("Manager");
            var customer = new IdentityRole("Student");

            if (!await roleManager.RoleExistsAsync("Manager"))
            {
                await roleManager.CreateAsync(manager);
            }

            if (!await roleManager.RoleExistsAsync("Student"))
            {
                await roleManager.CreateAsync(customer);
            }

            // Example: Create a default admin user
            var managerUser = new UserExtIdentity
            {
                FullName = "Henry Cano",
                UserName = "admin",
                StudentId = new Guid(),
                Email = "henry.canob@gmail.com",
            };

            var result = await userManager.CreateAsync(managerUser, "Password2025");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(managerUser, "Manager");
            }
        }

    }
}
