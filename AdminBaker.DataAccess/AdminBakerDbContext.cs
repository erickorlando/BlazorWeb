using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdminBaker.DataAccess
{
    public class AdminBakerDbContext : IdentityDbContext<IdentityUserECommerce>
    {
        public AdminBakerDbContext(DbContextOptions<AdminBakerDbContext> options)
            :base(options)
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

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}