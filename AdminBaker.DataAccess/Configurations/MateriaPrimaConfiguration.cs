using AdminBaker.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminBaker.DataAccess.Configurations;

public class MateriaPrimaConfiguration : IEntityTypeConfiguration<MateriaPrima>
{
    public void Configure(EntityTypeBuilder<MateriaPrima> builder)
    {
        builder.Property(p => p.Nombre)
            .HasMaxLength(100);
        
        builder.Property(p => p.Cantidad)
            .HasPrecision(11,2);

        builder.Property(p => p.Caducidad)
            .HasColumnType("date");
    }
}