using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class TratamientoMedicoConfiguration : IEntityTypeConfiguration<TratamientoMedico>
{
    public void Configure(EntityTypeBuilder<TratamientoMedico> builder)
    {
        builder.ToTable("tratamientoMedico");

        builder.Property(p => p.Id)
            .IsRequired();

        builder.Property(p => p.Dosis)
            .HasColumnName("dosis")
            .HasColumnType("int")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.FechaAdministracion)
            .HasColumnName("fechaAdministracion")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(p => p.Observacion)
            .HasColumnName("observacion")
            .HasColumnType("varchar")
            .HasMaxLength(300)
            .IsRequired();

        builder.HasOne(d => d.Cita)
            .WithMany(d => d.TratamientoMedicos)
            .HasForeignKey(d => d.IdCitaFk);

        builder.HasOne(d => d.Medicamento)
            .WithMany(d => d.TratamientoMedicos)
            .HasForeignKey(d => d.IdMedicamentoFk);
    }
}