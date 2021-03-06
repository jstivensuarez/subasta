﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Subasta.repository;

namespace Subasta.Migrations
{
    [DbContext(typeof(SubastaContext))]
    [Migration("20190130223144_agrego_precio_inicial_lote")]
    partial class agrego_precio_inicial_lote
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Subasta.repository.models.Animal", b =>
                {
                    b.Property<int>("AnimalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CODIGO_ANI");

                    b.Property<int>("CategoriaId")
                        .HasColumnName("COD_CATEGORIA_ANI");

                    b.Property<string>("Descripcion")
                        .HasColumnName("DESCRIPCION_ANI");

                    b.Property<string>("Foto")
                        .HasColumnName("FOTO_ANI");

                    b.Property<int>("LoteId")
                        .HasColumnName("COD_LOTE_ANI");

                    b.Property<int>("MunicipioId")
                        .HasColumnName("COD_MUN_PROCE_ANI");

                    b.Property<decimal>("Peso")
                        .HasColumnName("PESO_ANI");

                    b.Property<int>("RazaId")
                        .HasColumnName("COD_RAZA_ANI");

                    b.Property<int>("SexoId")
                        .HasColumnName("COD_SEXO_ANI");

                    b.HasKey("AnimalId");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("LoteId");

                    b.HasIndex("MunicipioId");

                    b.HasIndex("RazaId");

                    b.HasIndex("SexoId");

                    b.ToTable("TBL_ANIMALES");
                });

            modelBuilder.Entity("Subasta.repository.models.Categoria", b =>
                {
                    b.Property<int>("CategoriaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CODIGO_CAT");

                    b.Property<string>("Descripcion")
                        .HasColumnName("NOMBRE_CAT");

                    b.HasKey("CategoriaId");

                    b.ToTable("TBL_CATEGORIAS");
                });

            modelBuilder.Entity("Subasta.repository.models.Cliente", b =>
                {
                    b.Property<string>("ClienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID_CLI");

                    b.Property<string>("Correo")
                        .HasColumnName("CORREO_CLI");

                    b.Property<string>("Direccion")
                        .HasColumnName("DIRECCION_CLI");

                    b.Property<string>("Estado")
                        .HasColumnName("ESTADO_CLI");

                    b.Property<int>("MunicipioId")
                        .HasColumnName("CODIGO_MUN_CLI");

                    b.Property<string>("Nombre")
                        .HasColumnName("NOMBRE_CLI");

                    b.Property<string>("Representante")
                        .HasColumnName("REPRESENTANTE_LEGAL_CLI");

                    b.Property<string>("Telefono")
                        .HasColumnName("TELEFONO_CLI");

                    b.Property<int>("TipoDocumentoId")
                        .HasColumnName("CODIGO_TD_CLI");

                    b.Property<string>("Usuario")
                        .HasColumnName("USUARIO_CLI");

                    b.HasKey("ClienteId");

                    b.HasIndex("MunicipioId");

                    b.HasIndex("TipoDocumentoId");

                    b.ToTable("TBL_CLIENTES");
                });

            modelBuilder.Entity("Subasta.repository.models.Departamento", b =>
                {
                    b.Property<int>("DepartamentoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CODIGO_DPTO");

                    b.Property<string>("Descripcion")
                        .HasColumnName("NOMBRE_DPTO");

                    b.HasKey("DepartamentoId");

                    b.ToTable("TBL_DEPARTAMENTOS");
                });

            modelBuilder.Entity("Subasta.repository.models.Evento", b =>
                {
                    b.Property<int>("EventoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CODIGO_EVEN");

                    b.Property<string>("Descripcion")
                        .HasColumnName("NOMBRE_EVEN");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnName("FECHA_FIN_EVEN");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnName("FECHA_INI_EVEN");

                    b.Property<int>("MunicipioId")
                        .HasColumnName("UBICACION_EVEN");

                    b.HasKey("EventoId");

                    b.HasIndex("MunicipioId");

                    b.ToTable("TBL_EVENTOS");
                });

            modelBuilder.Entity("Subasta.repository.models.Lote", b =>
                {
                    b.Property<int>("LoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CODIGO_LOTE");

                    b.Property<int>("CantidadElementos")
                        .HasColumnName("CANTIDAD_ELEMENTOS_LOTE");

                    b.Property<string>("ClienteId")
                        .HasColumnName("ID_CLIENTE_LOTE");

                    b.Property<string>("Descripcion")
                        .HasColumnName("DESCRIPCION_LOTE");

                    b.Property<decimal>("FotoLote")
                        .HasColumnName("FOTO_LOTE");

                    b.Property<int>("MunicipioId")
                        .HasColumnName("COD_MUN_UBI_LOTE");

                    b.Property<string>("Nombre")
                        .HasColumnName("NOMBRE_LOTE");

                    b.Property<decimal>("PesoPromedio")
                        .HasColumnName("PESO_PROMEDIO_LOTE");

                    b.Property<decimal>("PesoTotal")
                        .HasColumnName("PESO_TOTAL_LOTE");

                    b.Property<decimal>("PrecioBase")
                        .HasColumnName("PRECIO_BASE_LOTE");

                    b.Property<decimal>("PrecioInicial")
                        .HasColumnName("PRECIO_INICIAL_LOTE");

                    b.Property<int>("SubastaId")
                        .HasColumnName("COD_SUBASTA_LOTE");

                    b.HasKey("LoteId");

                    b.HasIndex("ClienteId");

                    b.HasIndex("MunicipioId");

                    b.HasIndex("SubastaId");

                    b.ToTable("TBL_LOTES");
                });

            modelBuilder.Entity("Subasta.repository.models.Municipio", b =>
                {
                    b.Property<int>("MunicipioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CODIGO_MUN");

                    b.Property<int>("DepartamentoId")
                        .HasColumnName("COD_DPTO_MUN");

                    b.Property<string>("Descripcion")
                        .HasColumnName("NOMBRE_MUN");

                    b.HasKey("MunicipioId");

                    b.HasIndex("DepartamentoId");

                    b.ToTable("TBL_MUNICIPIOS");
                });

            modelBuilder.Entity("Subasta.repository.models.Puja", b =>
                {
                    b.Property<int>("PujaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CODIGO_PUJA");

                    b.Property<DateTime>("HoraPuja")
                        .HasColumnName("HORA_PUJA");

                    b.Property<string>("PujadorClienteId");

                    b.Property<int>("PujadorId")
                        .HasColumnName("COD_SUBASTA_PUJADOR");

                    b.Property<int?>("PujadorLoteId");

                    b.Property<decimal>("Valor")
                        .HasColumnName("VALOR_PUJA");

                    b.HasKey("PujaId");

                    b.HasIndex("PujadorClienteId", "PujadorLoteId");

                    b.ToTable("TBL_PUJAS");
                });

            modelBuilder.Entity("Subasta.repository.models.Pujador", b =>
                {
                    b.Property<string>("ClienteId")
                        .HasColumnName("ID_CLI_PUJADOR");

                    b.Property<int>("LoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CODIGO_LOTE_PUJADOR");

                    b.Property<string>("Banco")
                        .HasColumnName("BANCO_CONSIGNACION_PUJADOR");

                    b.Property<string>("Estado")
                        .HasColumnName("ESTADO_PUJADOR");

                    b.Property<string>("NumeroConsignacion")
                        .HasColumnName("NRO_CONSIGNACION_PUJADOR");

                    b.Property<decimal>("ValorConsignacion")
                        .HasColumnName("VALOR_CONSIGNACION_PUJADOR");

                    b.HasKey("ClienteId", "LoteId");

                    b.HasIndex("LoteId");

                    b.ToTable("TBL_PUJADORES");
                });

            modelBuilder.Entity("Subasta.repository.models.Raza", b =>
                {
                    b.Property<int>("RazaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CODIGO_RAZA");

                    b.Property<string>("Descripcion")
                        .HasColumnName("NOMBRE_RAZA");

                    b.HasKey("RazaId");

                    b.ToTable("TBL_RAZAS");
                });

            modelBuilder.Entity("Subasta.repository.models.Rol", b =>
                {
                    b.Property<int>("RolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CODIGO_ROL");

                    b.Property<string>("Descripcion")
                        .HasColumnName("DESCRIPCION_ROL");

                    b.Property<string>("Nombre")
                        .HasColumnName("NOMBRE_ROL");

                    b.HasKey("RolId");

                    b.ToTable("TBL_ROLES");
                });

            modelBuilder.Entity("Subasta.repository.models.Sexo", b =>
                {
                    b.Property<int>("SexoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CODIGO_SEXO");

                    b.Property<string>("Descripcion")
                        .HasColumnName("NOMBRE_SEXO");

                    b.HasKey("SexoId");

                    b.ToTable("TBL_SEXOS");
                });

            modelBuilder.Entity("Subasta.repository.models.Subasta", b =>
                {
                    b.Property<int>("SubastaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CODIGO_SUB");

                    b.Property<string>("Descripcion")
                        .HasColumnName("DESCRIPCION_SUB");

                    b.Property<int>("EventoId")
                        .HasColumnName("CODIGO_EVENTO_SUB");

                    b.Property<DateTime>("FechaLimite")
                        .HasColumnName("FECHA_HORA_INS_SUB");

                    b.Property<DateTime>("HoraFin")
                        .HasColumnName("FECHA_LIMITE_FIN_SUB");

                    b.Property<DateTime>("HoraInicio")
                        .HasColumnName("FECHA_HORA_INI_SUB");

                    b.Property<decimal>("ValorAnticipo")
                        .HasColumnName("VALOR_ANTICIPO_SUB");

                    b.HasKey("SubastaId");

                    b.HasIndex("EventoId");

                    b.ToTable("TBL_SUBASTAS");
                });

            modelBuilder.Entity("Subasta.repository.models.TipoDocumento", b =>
                {
                    b.Property<int>("TipoDocumentoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CODIGO_TD");

                    b.Property<string>("Descripcion")
                        .HasColumnName("NOMBRE_TD");

                    b.HasKey("TipoDocumentoId");

                    b.ToTable("TBL_TIPO_DOCUMENTOS");
                });

            modelBuilder.Entity("Subasta.repository.models.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CODIGO_USU");

                    b.Property<string>("Clave")
                        .HasColumnName("PASS_USUARIO");

                    b.Property<string>("Correo")
                        .HasColumnName("CORREO_USUARIO");

                    b.Property<string>("Nombre")
                        .HasColumnName("NOMBRE_USUARIO");

                    b.Property<int>("RolId")
                        .HasColumnName("ROL_USUARIO");

                    b.HasKey("UsuarioId");

                    b.HasIndex("RolId");

                    b.ToTable("TBL_USUARIOS");
                });

            modelBuilder.Entity("Subasta.repository.models.Animal", b =>
                {
                    b.HasOne("Subasta.repository.models.Categoria", "Categoria")
                        .WithMany("Animales")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Subasta.repository.models.Lote", "Lote")
                        .WithMany("Animales")
                        .HasForeignKey("LoteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Subasta.repository.models.Municipio", "Municipio")
                        .WithMany("Animales")
                        .HasForeignKey("MunicipioId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Subasta.repository.models.Raza", "Raza")
                        .WithMany("Animales")
                        .HasForeignKey("RazaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Subasta.repository.models.Sexo", "Sexo")
                        .WithMany("Animales")
                        .HasForeignKey("SexoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Subasta.repository.models.Cliente", b =>
                {
                    b.HasOne("Subasta.repository.models.Municipio", "Municipio")
                        .WithMany("Clientes")
                        .HasForeignKey("MunicipioId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Subasta.repository.models.TipoDocumento", "TipoDocumento")
                        .WithMany("Clientes")
                        .HasForeignKey("TipoDocumentoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Subasta.repository.models.Evento", b =>
                {
                    b.HasOne("Subasta.repository.models.Municipio", "Municipio")
                        .WithMany("Eventos")
                        .HasForeignKey("MunicipioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Subasta.repository.models.Lote", b =>
                {
                    b.HasOne("Subasta.repository.models.Cliente", "Cliente")
                        .WithMany("Lotes")
                        .HasForeignKey("ClienteId");

                    b.HasOne("Subasta.repository.models.Municipio", "Municipio")
                        .WithMany("Lotes")
                        .HasForeignKey("MunicipioId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Subasta.repository.models.Subasta", "Subasta")
                        .WithMany("Lotes")
                        .HasForeignKey("SubastaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Subasta.repository.models.Municipio", b =>
                {
                    b.HasOne("Subasta.repository.models.Departamento", "Departamento")
                        .WithMany("Municipios")
                        .HasForeignKey("DepartamentoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Subasta.repository.models.Puja", b =>
                {
                    b.HasOne("Subasta.repository.models.Pujador", "Pujador")
                        .WithMany("Pujas")
                        .HasForeignKey("PujadorClienteId", "PujadorLoteId");
                });

            modelBuilder.Entity("Subasta.repository.models.Pujador", b =>
                {
                    b.HasOne("Subasta.repository.models.Cliente", "Cliente")
                        .WithMany("Pujadores")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Subasta.repository.models.Lote", "Lote")
                        .WithMany("Pujadores")
                        .HasForeignKey("LoteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Subasta.repository.models.Subasta", b =>
                {
                    b.HasOne("Subasta.repository.models.Evento", "Evento")
                        .WithMany("Subastas")
                        .HasForeignKey("EventoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Subasta.repository.models.Usuario", b =>
                {
                    b.HasOne("Subasta.repository.models.Rol", "Rol")
                        .WithMany("Usuarios")
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
