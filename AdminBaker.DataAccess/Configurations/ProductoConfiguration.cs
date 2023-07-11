using AdminBaker.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminBaker.DataAccess.Configurations;

public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
{
    public void Configure(EntityTypeBuilder<Producto> builder)
    {
        builder.Property(p => p.Nombre)
            .HasMaxLength(150);

        builder.Property(p => p.Precio)
            .HasPrecision(11, 2);

        builder.Property(p => p.Relleno)
            .HasMaxLength(150);

        builder.Property(p => p.Tamanio)
            .HasPrecision(11, 2);
        
        builder.Property(p => p.ImagenUrl)
            .IsUnicode(false)
            .HasMaxLength(1000);

        builder.ToTable(nameof(Producto), o =>
        {
            o.IsTemporal();
        });
    }
}