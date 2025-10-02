using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniERP.Models;

namespace MiniERP.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Usuario>>();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Asegura la BD/migraciones
            await context.Database.MigrateAsync();

            // Roles
            string[] roles = new[] { "Admin", "RRHH", "Operario" };
            foreach (var r in roles)
            {
                if (!await roleManager.RoleExistsAsync(r))
                    await roleManager.CreateAsync(new IdentityRole(r));
            }

            // Usuarios de ejemplo
            await CreateUserIfNotExists(userManager, "admin@erp.com", "Admin123!", "Admin");
            await CreateUserIfNotExists(userManager, "rrhh@erp.com", "Rrhh123!", "RRHH");
            await CreateUserIfNotExists(userManager, "operario@erp.com", "Operario123!", "Operario");
        }

        private static async Task CreateUserIfNotExists(UserManager<Usuario> userManager, string email, string password, string role)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new Usuario
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}
