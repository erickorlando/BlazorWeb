using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace AdminBaker.DataAccess
{
    public class AdminBakerDbContext : DbContext
    {
        public AdminBakerDbContext(DbContextOptions<AdminBakerDbContext> options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}