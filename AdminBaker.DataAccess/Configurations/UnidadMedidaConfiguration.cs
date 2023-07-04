using AdminBaker.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminBaker.DataAccess.Configurations;

public class UnidadMedidaConfiguration : IEntityTypeConfiguration<UnidadMedida>
{
    public void Configure(EntityTypeBuilder<UnidadMedida> builder)
    {
        builder.Property(p => p.Codigo)
            .HasMaxLength(10);
        
        builder.Property(p => p.Descripcion)
            .HasMaxLength(100);

        builder.HasData(new List<UnidadMedida>()
        {
            new() { Id = 1, Codigo = "Und.", Descripcion = "Unidades" },
            new() { Id = 2, Codigo = "Gr.", Descripcion = "Gramos" },
            new() { Id = 3, Codigo = "Kg.", Descripcion = "Kilos" },
            new() { Id = 4, Codigo = "Bot.", Descripcion = "Botella" },
            new() { Id = 5, Codigo = "Fra.", Descripcion = "Frasco" },
            new() { Id = 6, Codigo = "Bol", Descripcion = "Bolsa" },
        });
    }
}