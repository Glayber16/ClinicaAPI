﻿// <auto-generated />
using System;
using ClinicaAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ClinicaAPI.Migrations
{
    [DbContext(typeof(ClinicaDBContext))]
    [Migration("20241128132417_AddPacienteRelationshipToMedicalRecord")]
    partial class AddPacienteRelationshipToMedicalRecord
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ClinicaAPI.Model.MedicalRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PacienteId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PacienteId");

                    b.ToTable("Prontuarios");
                });

            modelBuilder.Entity("ClinicaAPI.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SenhaHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TipoUsuario")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Usuarios", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("ClinicaAPI.Model.Doctor", b =>
                {
                    b.HasBaseType("ClinicaAPI.Model.User");

                    b.Property<string>("CRM")
                        .IsRequired()
                        .HasColumnType("text");

                    b.ToTable("Medicos", (string)null);
                });

            modelBuilder.Entity("ClinicaAPI.Model.Patient", b =>
                {
                    b.HasBaseType("ClinicaAPI.Model.User");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("timestamp with time zone");

                    b.ToTable("Pacientes", (string)null);
                });

            modelBuilder.Entity("ClinicaAPI.Model.MedicalRecord", b =>
                {
                    b.HasOne("ClinicaAPI.Model.Patient", "Paciente")
                        .WithMany("Prontuarios")
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("ClinicaAPI.Model.Doctor", b =>
                {
                    b.HasOne("ClinicaAPI.Model.User", null)
                        .WithOne()
                        .HasForeignKey("ClinicaAPI.Model.Doctor", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClinicaAPI.Model.Patient", b =>
                {
                    b.HasOne("ClinicaAPI.Model.User", null)
                        .WithOne()
                        .HasForeignKey("ClinicaAPI.Model.Patient", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClinicaAPI.Model.Patient", b =>
                {
                    b.Navigation("Prontuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
