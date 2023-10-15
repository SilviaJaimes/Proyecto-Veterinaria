using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class MovimientoMedicamentoConfiguration : IEntityTypeConfiguration<MovimientoMedicamento>
{
    public void Configure(EntityTypeBuilder<MovimientoMedicamento> builder)
    {
        builder.ToTable("movimientoMedicamento");

        builder.Property(p => p.Id)
            .IsRequired();

        builder.Property(p => p.Cantidad)
            .HasColumnName("cantidad")
            .HasColumnType("int")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.Fecha)
            .HasColumnName("fecha")
            .HasColumnType("date")
            .IsRequired();

        builder.HasOne(d => d.TipoMovimiento)
            .WithMany(d => d.MovimientoMedicamentos)
            .HasForeignKey(d => d.IdTipoMov);
    }
}