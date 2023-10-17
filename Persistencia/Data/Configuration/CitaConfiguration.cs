using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class CitaConfiguration : IEntityTypeConfiguration<Cita>
{
    public void Configure(EntityTypeBuilder<Cita> builder)
    {
        builder.ToTable("cita");

        builder.HasKey(x => x.Id);

        builder.Property(p => p.Id)
            .IsRequired();

        builder.Property(p => p.Fecha)
            .HasColumnName("fecha")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(p => p.Hora)
            .HasColumnName("hora")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(p => p.Motivo)
            .HasColumnName("motivo")
            .HasColumnType("varchar")
            .HasMaxLength(300)
            .IsRequired();

        builder.HasOne(d => d.Mascota)
            .WithMany(d => d.Citas)
            .HasForeignKey(d => d.IdMascotaFk);

        builder.HasOne(d => d.Veterinario)
            .WithMany(d => d.Citas)
            .HasForeignKey(d => d.IdVeterinarioFk);
    }
}