﻿using System.Reflection;
using AdminBaker.Entities.Info;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdminBaker.DataAccess
{
    public class AdminBakerDbContext : IdentityDbContext<IdentityUserECommerce>
    {
        public AdminBakerDbContext(DbContextOptions<AdminBakerDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // AspNetUsers
            modelBuilder.Entity<IdentityUserECommerce>(e =>
            {
                e.ToTable(name: "Usuario");
                e.Property(p => p.FechaNacimiento)
                    .HasColumnType("date");
            });

            // AspNetRoles
            modelBuilder.Entity<IdentityRole>(e =>
            {
                e.ToTable(name: "Rol");
            });

            // AspNetUserRoles
            modelBuilder.Entity<IdentityUserRole<string>>(e =>
            {
                e.ToTable("UsuarioRol");
            });

            modelBuilder.Entity<PedidoInfo>()
                .HasNoKey()
                .Property(p => p.TotalVenta)
                .HasPrecision(11, 2);
            
            modelBuilder.Entity<PedidoInfo>()
                .Property(p => p.Cantidad)
                .HasPrecision(11, 2);
            
            modelBuilder.Entity<PedidoInfo>()
                .Property(p => p.Precio)
                .HasPrecision(11, 2);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}