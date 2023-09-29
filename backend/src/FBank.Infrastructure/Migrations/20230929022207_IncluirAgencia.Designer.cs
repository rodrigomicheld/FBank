﻿// <auto-generated />
using System;
using FBank.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FBank.Infrastructure.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20230929022207_IncluirAgencia")]
    partial class IncluirAgencia
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FBank.Domain.Entities.Agency", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("codigo_agencia");

                    b.Property<int>("BankCode")
                        .HasColumnType("int")
                        .HasColumnName("codigo_banco");

                    b.Property<DateTime>("CreateDateAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("criado_em");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("nome");

                    b.Property<DateTime>("UpdateDateAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("atualizado_em");

                    b.HasKey("Id");

                    b.ToTable("Agency", (string)null);
                });

            modelBuilder.Entity("FBank.Domain.Entities.Bank", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("codigo");

                    b.Property<DateTime>("CreateDateAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("criado_em");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("nome");

                    b.Property<DateTime>("UpdateDateAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("atualizado_em");

                    b.HasKey("Id");

                    b.ToTable("Banco", (string)null);
                });

            modelBuilder.Entity("FBank.Domain.Entities.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreateDateAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("criado_em");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("nome");

                    b.Property<DateTime>("UpdateDateAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("atualizado_em");

                    b.HasKey("Id");

                    b.ToTable("Cliente", (string)null);
                });

            modelBuilder.Entity("FBank.Domain.Entities.Agency", b =>
                {
                    b.HasOne("FBank.Domain.Entities.Bank", "Bank")
                        .WithMany("Agencies")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bank");
                });

            modelBuilder.Entity("FBank.Domain.Entities.Bank", b =>
                {
                    b.Navigation("Agencies");
                });
#pragma warning restore 612, 618
        }
    }
}
