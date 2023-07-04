using AdminBaker.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminBaker.DataAccess.Configurations;

public class TipoTortaConfiguration : IEntityTypeConfiguration<TipoTorta>
{
    public void Configure(EntityTypeBuilder<TipoTorta> builder)
    {
        builder.Property(p => p.Nombre)
            .HasMaxLength(100);
    }
}