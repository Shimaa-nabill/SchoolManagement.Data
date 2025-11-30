using Microsoft.AspNetCore.Identity;
using SchoolManagement.Data.Entities;

namespace SchoolManagement.Web.Helper
{
    public static class SeedData
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<long>>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roles = new[] { "Admin", "Teacher", "Student" };
            foreach (var r in roles)
                if (!await roleManager.RoleExistsAsync(r))
                    await roleManager.CreateAsync(new IdentityRole<long>(r));


            var adminEmail = "admin@school.com";
            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (admin == null)
            {
                var a = new ApplicationUser { Email = adminEmail, UserName = adminEmail, Name = "Shimaa" };
                await userManager.CreateAsync(a, "Password$$!123");
                await userManager.AddToRoleAsync(a, "Admin");
            }
        }
    }
}
