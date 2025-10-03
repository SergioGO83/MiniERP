using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniERP.Data;
using MiniERP.Models;

namespace MiniERP.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<Usuario>>();

            await context.Database.MigrateAsync();

            // ============================
            // 1) Crear roles
            // ============================
            string[] roles = { "Admin", "RRHH", "Operario" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // ============================
            // 2) Crear usuarios
            // ============================
            if (await userManager.FindByEmailAsync("admin@erp.com") == null)
            {
                var user = new Usuario { UserName = "admin@erp.com", Email = "admin@erp.com", EmailConfirmed = true };
                await userManager.CreateAsync(user, "Admin123!");
                await userManager.AddToRoleAsync(user, "Admin");
            }

            if (await userManager.FindByEmailAsync("rrhh@erp.com") == null)
            {
                var user = new Usuario { UserName = "rrhh@erp.com", Email = "rrhh@erp.com", EmailConfirmed = true };
                await userManager.CreateAsync(user, "Rrhh123!");
                await userManager.AddToRoleAsync(user, "RRHH");
            }

            if (await userManager.FindByEmailAsync("operario@erp.com") == null)
            {
                var user = new Usuario { UserName = "operario@erp.com", Email = "operario@erp.com", EmailConfirmed = true };
                await userManager.CreateAsync(user, "Operario123!");
                await userManager.AddToRoleAsync(user, "Operario");
            }

            // ============================
            // 3) Departamentos
            // ============================
            if (!context.Departamentos.Any())
            {
                context.Departamentos.AddRange(
                    new Departamento { Nombre = "RRHH" },
                    new Departamento { Nombre = "Almacén" }
                );
                await context.SaveChangesAsync();
            }

            // ============================
            // 4) Categorías
            // ============================
            if (!context.Categorias.Any())
            {
                context.Categorias.AddRange(
                    new Categoria { Nombre = "Electrónica" },
                    new Categoria { Nombre = "Alimentación" },
                    new Categoria { Nombre = "Ropa" }
                );
                await context.SaveChangesAsync();
            }

            // ============================
            // 5) Empleados
            // ============================
            if (!context.Empleados.Any())
            {
                var rrhh = context.Departamentos.FirstOrDefault(d => d.Nombre == "RRHH");
                var almacen = context.Departamentos.FirstOrDefault(d => d.Nombre == "Almacén");

                context.Empleados.AddRange(
                    new Empleado { Nombre = "Jessica", Edad = 42, DepartamentoId = rrhh.Id },
                    new Empleado { Nombre = "Keko", Edad = 35, DepartamentoId = almacen.Id }
                );
                await context.SaveChangesAsync();
            }

            // ============================
            // 6) Productos
            // ============================
            if (!context.Productos.Any())
            {
                var electronica = context.Categorias.FirstOrDefault(c => c.Nombre == "Electrónica");
                var alimentacion = context.Categorias.FirstOrDefault(c => c.Nombre == "Alimentación");

                context.Productos.AddRange(
                    new Producto { Nombre = "Portátil Lenovo", Stock = 10, Precio = 800, CategoriaId = electronica.Id },
                    new Producto { Nombre = "Smartphone Samsung", Stock = 20, Precio = 500, CategoriaId = electronica.Id },
                    new Producto { Nombre = "Leche sin lactosa", Stock = 50, Precio = 1.5M, CategoriaId = alimentacion.Id }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
