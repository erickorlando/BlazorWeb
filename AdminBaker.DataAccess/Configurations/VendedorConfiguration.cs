using AdminBaker.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminBaker.DataAccess.Configurations;

public class VendedorConfiguration : IEntityTypeConfiguration<Vendedor>
{
    public void Configure(EntityTypeBuilder<Vendedor> builder)
    {
        builder.Property(p => p.CodigoTrabajador)
            .HasMaxLength(100);
        
        builder.Property(p => p.Horario)
            .HasMaxLength(500);
    }
}