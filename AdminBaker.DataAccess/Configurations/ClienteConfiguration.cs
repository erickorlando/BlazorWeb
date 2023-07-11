using AdminBaker.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminBaker.DataAccess.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder
            .Property(p => p.FechaNacimiento)
            .HasColumnType("date");

        builder.ToTable(nameof(Cliente), o =>
        {
            o.IsTemporal();
        });
    }
}