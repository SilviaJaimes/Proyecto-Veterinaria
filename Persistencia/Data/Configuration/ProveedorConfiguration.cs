using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class ProveedorConfiguration : IEntityTypeConfiguration<Proveedor>
{
    public void Configure(EntityTypeBuilder<Proveedor> builder)
    {
        builder.ToTable("proveedor");

        builder.HasKey(x => x.Id);

        builder.Property(p => p.Id)
            .IsRequired();

        builder.Property(p => p.Nombre)
            .HasColumnName("nombre")
            .HasColumnType("varchar")
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(p => p.Direccion)
            .HasColumnName("direccion")
            .HasColumnType("varchar")
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(p => p.Telefono)
            .HasColumnName("telefono")
            .HasColumnType("varchar")
            .HasMaxLength(250)
            .IsRequired();

        builder
            .HasMany(p => p.Medicamentos)
            .WithMany(r => r.Proveedores)
            .UsingEntity<MedicamentoProveedor>(

                j => j
                .HasOne(pt => pt.Medicamento)
                .WithMany(t => t.MedicamentoProveedores)
                .HasForeignKey(ut => ut.IdMedicamentoFk),

                j => j
                .HasOne(et => et.Proveedor)
                .WithMany(et => et.MedicamentoProveedores)
                .HasForeignKey(el => el.IdProveedorFk),

                j =>
                {
                    j.ToTable("medicamentoProveedor");
                    j.HasKey(t => new { t.IdProveedorFk, t.IdMedicamentoFk });
                });
    }
}