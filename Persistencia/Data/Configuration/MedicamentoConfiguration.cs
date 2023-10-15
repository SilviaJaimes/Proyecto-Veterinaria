using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class MedicamentoConfiguration : IEntityTypeConfiguration<Medicamento>
{
    public void Configure(EntityTypeBuilder<Medicamento> builder)
    {
        builder.ToTable("medicamento");

        builder.Property(p => p.Id)
            .IsRequired();

        builder.Property(p => p.Nombre)
            .HasColumnName("nombre")
            .HasColumnType("varchar")
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(p => p.CantidadDisponible)
            .HasColumnName("cantidadDisponible")
            .HasColumnType("int")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.Precio)
            .HasColumnName("precio")
            .HasColumnType("double")
            .HasMaxLength(300)
            .IsRequired();

        builder.HasOne(d => d.Laboratorio)
            .WithMany(d => d.Medicamentos)
            .HasForeignKey(d => d.IdLaboratorioFk);
    }
}