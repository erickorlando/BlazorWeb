using AdminBaker.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminBaker.DataAccess.Configurations;

public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.Property(p => p.Fecha)
            .HasColumnType("date");
        
        builder.Property(p => p.FechaRetiro)
            .HasColumnType("date");

        builder.Property(p => p.TotalVenta)
            .HasPrecision(11, 2);

        builder.Property(p => p.UrlImagen)
            .IsUnicode(false)
            .HasMaxLength(1000);

        builder.Property(p => p.NroPedido)
            .HasMaxLength(30);
        
        builder.Property(p => p.MensajePersonalizado)
            .HasMaxLength(500);

        builder.ToTable(nameof(Pedido), o =>
        {
            o.IsTemporal();
        });
    }
}