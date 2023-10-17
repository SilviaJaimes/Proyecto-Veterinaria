using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class MascotaConfiguration : IEntityTypeConfiguration<Mascota>
{
    public void Configure(EntityTypeBuilder<Mascota> builder)
    {
        builder.ToTable("mascota");

        builder.HasKey(x => x.Id);

        builder.Property(p => p.Id)
            .IsRequired();

        builder.Property(p => p.Nombre)
            .HasColumnName("nombre")
            .HasColumnType("varchar")
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(p => p.FechaNacimiento)
            .HasColumnName("fechaNacimiento")
            .HasColumnType("date")
            .IsRequired();

        builder.HasOne(d => d.Propietario)
            .WithMany(d => d.Mascotas)
            .HasForeignKey(d => d.IdPropietarioFk);

        builder
            .HasOne(e => e.Raza)
            .WithOne(p => p.Mascotas)
            .HasForeignKey<Mascota>(p => p.IdRazaFk);
    }
}