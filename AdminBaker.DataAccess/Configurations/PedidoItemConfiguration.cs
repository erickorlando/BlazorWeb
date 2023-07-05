using AdminBaker.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminBaker.DataAccess.Configurations;

public class PedidoItemConfiguration : IEntityTypeConfiguration<PedidoItem>
{
    public void Configure(EntityTypeBuilder<PedidoItem> builder)
    {
        builder.Property(p => p.Cantidad)
            .HasPrecision(11, 2);
        
        builder.Property(p => p.PrecioUnitario)
            .HasPrecision(11, 2);

        builder.Property(p => p.Relleno)
            .HasMaxLength(150);

        builder.Property(p => p.Tamanio)
            .HasPrecision(11, 2);
        
        builder.Property(p => p.Total)
            .HasPrecision(11, 2);
        
    }
}