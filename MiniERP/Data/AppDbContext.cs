
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniERP.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MiniERP.Data
{
    public class AppDbContext : IdentityDbContext<Usuario>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed de datos iniciales
            builder.Entity<Departamento>().HasData(
                new Departamento { Id = 1, Nombre = "RRHH" },
                new Departamento { Id = 2, Nombre = "Almacén" }
            );

            builder.Entity<Categoria>().HasData(
                new Categoria { Id = 1, Nombre = "Electrónica" },
                new Categoria { Id = 2, Nombre = "Alimentación" }
            );
        }
    }
}