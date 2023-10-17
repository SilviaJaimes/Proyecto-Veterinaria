using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class DetalleMovimientoConfiguration : IEntityTypeConfiguration<DetalleMovimiento>
{
    public void Configure(EntityTypeBuilder<DetalleMovimiento> builder)
    {
        builder.ToTable("detalleMovimiento");

        builder.HasKey(x => x.Id);

        builder.Property(p => p.Id)
            .IsRequired();

        builder.Property(p => p.Cantidad)
            .HasColumnName("cantidad")
            .HasColumnType("int")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.Precio)
            .HasColumnName("precio")
            .HasColumnType("double")
            .HasMaxLength(300)
            .IsRequired();

        builder.HasOne(d => d.Medicamento)
            .WithMany(d => d.DetalleMovimientos)
            .HasForeignKey(d => d.IdMedicamentoFk);

        builder.HasOne(d => d.MovimientoMedicamento)
            .WithMany(d => d.DetalleMovimientos)
            .HasForeignKey(d => d.IdMovMedFk);
    }
}