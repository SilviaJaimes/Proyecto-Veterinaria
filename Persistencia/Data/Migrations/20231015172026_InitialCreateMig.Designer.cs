﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistencia;

#nullable disable

namespace Persistencia.Data.Migrations
{
    [DbContext(typeof(ApiContext))]
    [Migration("20231015172026_InitialCreateMig")]
    partial class InitialCreateMig
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Dominio.Entities.Cita", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateOnly>("Fecha")
                        .HasColumnType("date")
                        .HasColumnName("fecha");

                    b.Property<DateTime>("Hora")
                        .HasColumnType("date")
                        .HasColumnName("hora");

                    b.Property<int>("IdMascotaFk")
                        .HasColumnType("int");

                    b.Property<int>("IdVeterinarioFk")
                        .HasColumnType("int");

                    b.Property<string>("Motivo")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar")
                        .HasColumnName("motivo");

                    b.HasKey("Id");

                    b.HasIndex("IdMascotaFk");

                    b.HasIndex("IdVeterinarioFk");

                    b.ToTable("cita", (string)null);
                });

            modelBuilder.Entity("Dominio.Entities.DetalleMovimiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Cantidad")
                        .HasMaxLength(50)
                        .HasColumnType("int")
                        .HasColumnName("cantidad");

                    b.Property<int>("IdMedicamentoFk")
                        .HasColumnType("int");

                    b.Property<int>("IdMovMedFk")
                        .HasColumnType("int");

                    b.Property<double>("Precio")
                        .HasMaxLength(300)
                        .HasColumnType("double")
                        .HasColumnName("precio");

                    b.HasKey("Id");

                    b.HasIndex("IdMedicamentoFk");

                    b.HasIndex("IdMovMedFk");

                    b.ToTable("detalleMovimiento", (string)null);
                });

            modelBuilder.Entity("Dominio.Entities.Especie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar")
                        .HasColumnName("nombre");

                    b.HasKey("Id");

                    b.ToTable("especie", (string)null);
                });

            modelBuilder.Entity("Dominio.Entities.Laboratorio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar")
                        .HasColumnName("direccion");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar")
                        .HasColumnName("nombre");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar")
                        .HasColumnName("telefono");

                    b.HasKey("Id");

                    b.ToTable("laboratorio", (string)null);
                });

            modelBuilder.Entity("Dominio.Entities.Mascota", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateOnly>("FechaNacimiento")
                        .HasColumnType("date")
                        .HasColumnName("fechaNacimiento");

                    b.Property<int>("IdPropietarioFk")
                        .HasColumnType("int");

                    b.Property<int>("IdRazaFk")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar")
                        .HasColumnName("nombre");

                    b.HasKey("Id");

                    b.HasIndex("IdPropietarioFk");

                    b.HasIndex("IdRazaFk")
                        .IsUnique();

                    b.ToTable("mascota", (string)null);
                });

            modelBuilder.Entity("Dominio.Entities.Medicamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CantidadDisponible")
                        .HasMaxLength(50)
                        .HasColumnType("int")
                        .HasColumnName("cantidadDisponible");

                    b.Property<int>("IdLaboratorioFk")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar")
                        .HasColumnName("nombre");

                    b.Property<double>("Precio")
                        .HasMaxLength(300)
                        .HasColumnType("double")
                        .HasColumnName("precio");

                    b.HasKey("Id");

                    b.HasIndex("IdLaboratorioFk");

                    b.ToTable("medicamento", (string)null);
                });

            modelBuilder.Entity("Dominio.Entities.MedicamentoProveedor", b =>
                {
                    b.Property<int>("IdProveedorFk")
                        .HasColumnType("int");

                    b.Property<int>("IdMedicamentoFk")
                        .HasColumnType("int");

                    b.HasKey("IdProveedorFk", "IdMedicamentoFk");

                    b.HasIndex("IdMedicamentoFk");

                    b.ToTable("medicamentoProveedor", (string)null);
                });

            modelBuilder.Entity("Dominio.Entities.MovimientoMedicamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Cantidad")
                        .HasMaxLength(50)
                        .HasColumnType("int")
                        .HasColumnName("cantidad");

                    b.Property<DateOnly>("Fecha")
                        .HasColumnType("date")
                        .HasColumnName("fecha");

                    b.Property<int>("IdTipoMov")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdTipoMov");

                    b.ToTable("movimientoMedicamento", (string)null);
                });

            modelBuilder.Entity("Dominio.Entities.Propietario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CorreoElectronico")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar")
                        .HasColumnName("correoElectronico");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar")
                        .HasColumnName("nombre");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar")
                        .HasColumnName("telefono");

                    b.HasKey("Id");

                    b.ToTable("propietario", (string)null);
                });

            modelBuilder.Entity("Dominio.Entities.Proveedor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar")
                        .HasColumnName("direccion");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar")
                        .HasColumnName("nombre");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar")
                        .HasColumnName("telefono");

                    b.HasKey("Id");

                    b.ToTable("proveedor", (string)null);
                });

            modelBuilder.Entity("Dominio.Entities.Raza", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("IdEspecieFk")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar")
                        .HasColumnName("nombre");

                    b.HasKey("Id");

                    b.HasIndex("IdEspecieFk");

                    b.ToTable("raza", (string)null);
                });

            modelBuilder.Entity("Dominio.Entities.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("Revoked")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Token")
                        .HasColumnType("longtext");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("RefreshToken", (string)null);
                });

            modelBuilder.Entity("Dominio.Entities.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar")
                        .HasColumnName("rolName");

                    b.HasKey("Id");

                    b.ToTable("rol", (string)null);
                });

            modelBuilder.Entity("Dominio.Entities.RolUsuario", b =>
                {
                    b.Property<int>("IdUsuarioFk")
                        .HasColumnType("int");

                    b.Property<int>("IdRolFk")
                        .HasColumnType("int");

                    b.HasKey("IdUsuarioFk", "IdRolFk");

                    b.HasIndex("IdRolFk");

                    b.ToTable("rolUsuario", (string)null);
                });

            modelBuilder.Entity("Dominio.Entities.TipoMovimiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar")
                        .HasColumnName("descripcion");

                    b.HasKey("Id");

                    b.ToTable("tipoMovimiento", (string)null);
                });

            modelBuilder.Entity("Dominio.Entities.TratamientoMedico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Dosis")
                        .HasMaxLength(100)
                        .HasColumnType("int")
                        .HasColumnName("dosis");

                    b.Property<DateOnly>("FechaAdministracion")
                        .HasColumnType("date")
                        .HasColumnName("fechaAdministracion");

                    b.Property<int>("IdCitaFk")
                        .HasColumnType("int");

                    b.Property<int>("IdMedicamentoFk")
                        .HasColumnType("int");

                    b.Property<string>("Observacion")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar")
                        .HasColumnName("observacion");

                    b.HasKey("Id");

                    b.HasIndex("IdCitaFk");

                    b.HasIndex("IdMedicamentoFk");

                    b.ToTable("tratamientoMedico", (string)null);
                });

            modelBuilder.Entity("Dominio.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Contraseña")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar")
                        .HasColumnName("contraseña");

                    b.Property<string>("CorreoElectronico")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar")
                        .HasColumnName("correoElectronico");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar")
                        .HasColumnName("nombre");

                    b.HasKey("Id");

                    b.ToTable("usuario", (string)null);
                });

            modelBuilder.Entity("Dominio.Entities.Veterinario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar")
                        .HasColumnName("direccion");

                    b.Property<string>("Especialidad")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar")
                        .HasColumnName("especialidad");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar")
                        .HasColumnName("nombre");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar")
                        .HasColumnName("telefono");

                    b.HasKey("Id");

                    b.ToTable("veterinario", (string)null);
                });

            modelBuilder.Entity("Dominio.Entities.Cita", b =>
                {
                    b.HasOne("Dominio.Entities.Mascota", "Mascota")
                        .WithMany("Citas")
                        .HasForeignKey("IdMascotaFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Entities.Veterinario", "Veterinario")
                        .WithMany("Citas")
                        .HasForeignKey("IdVeterinarioFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mascota");

                    b.Navigation("Veterinario");
                });

            modelBuilder.Entity("Dominio.Entities.DetalleMovimiento", b =>
                {
                    b.HasOne("Dominio.Entities.Medicamento", "Medicamento")
                        .WithMany("DetalleMovimientos")
                        .HasForeignKey("IdMedicamentoFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Entities.MovimientoMedicamento", "MovimientoMedicamento")
                        .WithMany("DetalleMovimientos")
                        .HasForeignKey("IdMovMedFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medicamento");

                    b.Navigation("MovimientoMedicamento");
                });

            modelBuilder.Entity("Dominio.Entities.Mascota", b =>
                {
                    b.HasOne("Dominio.Entities.Propietario", "Propietario")
                        .WithMany("Mascotas")
                        .HasForeignKey("IdPropietarioFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Entities.Raza", "Raza")
                        .WithOne("Mascotas")
                        .HasForeignKey("Dominio.Entities.Mascota", "IdRazaFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Propietario");

                    b.Navigation("Raza");
                });

            modelBuilder.Entity("Dominio.Entities.Medicamento", b =>
                {
                    b.HasOne("Dominio.Entities.Laboratorio", "Laboratorio")
                        .WithMany("Medicamentos")
                        .HasForeignKey("IdLaboratorioFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Laboratorio");
                });

            modelBuilder.Entity("Dominio.Entities.MedicamentoProveedor", b =>
                {
                    b.HasOne("Dominio.Entities.Medicamento", "Medicamento")
                        .WithMany("MedicamentoProveedores")
                        .HasForeignKey("IdMedicamentoFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Entities.Proveedor", "Proveedor")
                        .WithMany("MedicamentoProveedores")
                        .HasForeignKey("IdProveedorFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medicamento");

                    b.Navigation("Proveedor");
                });

            modelBuilder.Entity("Dominio.Entities.MovimientoMedicamento", b =>
                {
                    b.HasOne("Dominio.Entities.TipoMovimiento", "TipoMovimiento")
                        .WithMany("MovimientoMedicamentos")
                        .HasForeignKey("IdTipoMov")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoMovimiento");
                });

            modelBuilder.Entity("Dominio.Entities.Raza", b =>
                {
                    b.HasOne("Dominio.Entities.Especie", "Especie")
                        .WithMany("Razas")
                        .HasForeignKey("IdEspecieFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Especie");
                });

            modelBuilder.Entity("Dominio.Entities.RefreshToken", b =>
                {
                    b.HasOne("Dominio.Entities.Usuario", "Usuario")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Dominio.Entities.RolUsuario", b =>
                {
                    b.HasOne("Dominio.Entities.Rol", "Rol")
                        .WithMany("RolUsuarios")
                        .HasForeignKey("IdRolFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Entities.Usuario", "Usuario")
                        .WithMany("RolUsuarios")
                        .HasForeignKey("IdUsuarioFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rol");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Dominio.Entities.TratamientoMedico", b =>
                {
                    b.HasOne("Dominio.Entities.Cita", "Cita")
                        .WithMany("TratamientoMedicos")
                        .HasForeignKey("IdCitaFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Entities.Medicamento", "Medicamento")
                        .WithMany("TratamientoMedicos")
                        .HasForeignKey("IdMedicamentoFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cita");

                    b.Navigation("Medicamento");
                });

            modelBuilder.Entity("Dominio.Entities.Cita", b =>
                {
                    b.Navigation("TratamientoMedicos");
                });

            modelBuilder.Entity("Dominio.Entities.Especie", b =>
                {
                    b.Navigation("Razas");
                });

            modelBuilder.Entity("Dominio.Entities.Laboratorio", b =>
                {
                    b.Navigation("Medicamentos");
                });

            modelBuilder.Entity("Dominio.Entities.Mascota", b =>
                {
                    b.Navigation("Citas");
                });

            modelBuilder.Entity("Dominio.Entities.Medicamento", b =>
                {
                    b.Navigation("DetalleMovimientos");

                    b.Navigation("MedicamentoProveedores");

                    b.Navigation("TratamientoMedicos");
                });

            modelBuilder.Entity("Dominio.Entities.MovimientoMedicamento", b =>
                {
                    b.Navigation("DetalleMovimientos");
                });

            modelBuilder.Entity("Dominio.Entities.Propietario", b =>
                {
                    b.Navigation("Mascotas");
                });

            modelBuilder.Entity("Dominio.Entities.Proveedor", b =>
                {
                    b.Navigation("MedicamentoProveedores");
                });

            modelBuilder.Entity("Dominio.Entities.Raza", b =>
                {
                    b.Navigation("Mascotas");
                });

            modelBuilder.Entity("Dominio.Entities.Rol", b =>
                {
                    b.Navigation("RolUsuarios");
                });

            modelBuilder.Entity("Dominio.Entities.TipoMovimiento", b =>
                {
                    b.Navigation("MovimientoMedicamentos");
                });

            modelBuilder.Entity("Dominio.Entities.Usuario", b =>
                {
                    b.Navigation("RefreshTokens");

                    b.Navigation("RolUsuarios");
                });

            modelBuilder.Entity("Dominio.Entities.Veterinario", b =>
                {
                    b.Navigation("Citas");
                });
#pragma warning restore 612, 618
        }
    }
}
