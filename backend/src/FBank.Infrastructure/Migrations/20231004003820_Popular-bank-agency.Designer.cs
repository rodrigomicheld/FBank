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
    [Migration("20231004003820_Popular-bank-agency")]
    partial class Popularbankagency
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FBank.Domain.Entities.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("AgencyId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Agencia_id");

                    b.Property<decimal>("Balance")
                        .HasColumnType("Decimal(21,9)")
                        .HasColumnName("Balance");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Cliente_id");

                    b.Property<DateTime>("CreateDateAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("criado_em");

                    b.Property<int>("IdStatus")
                        .HasColumnType("int")
                        .HasColumnName("Status_Id");

                    b.Property<DateTime>("UpdateDateAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("atualizado_em");

                    b.HasKey("Id");

                    b.HasIndex("AgencyId");

                    b.HasIndex("ClientId");

                    b.ToTable("Conta", (string)null);
                });

            modelBuilder.Entity("FBank.Domain.Entities.Agency", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("BankId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("banco_id");

                    b.Property<int>("Code")
                        .HasColumnType("int")
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

                    b.HasIndex("BankId");

                    b.ToTable("Agencia", (string)null);
                });

            modelBuilder.Entity("FBank.Domain.Entities.Bank", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<int>("Code")
                        .HasColumnType("int")
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

                    b.HasAlternateKey("Code");

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

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("documento");

                    b.Property<int>("PersonType")
                        .HasColumnType("int")
                        .HasColumnName("tipo_documento");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("nome");

                    b.Property<DateTime>("UpdateDateAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("atualizado_em");

                    b.HasKey("Id");

                    b.HasAlternateKey("Document");

                    b.ToTable("Cliente", (string)null);
                });

            modelBuilder.Entity("FBank.Domain.Entities.Account", b =>
                {
                    b.HasOne("FBank.Domain.Entities.Agency", "Agency")
                        .WithMany("Accounts")
                        .HasForeignKey("AgencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FBank.Domain.Entities.Client", "Client")
                        .WithMany("Accounts")
                        .HasForeignKey("ClientId");

                    b.Navigation("Agency");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("FBank.Domain.Entities.Agency", b =>
                {
                    b.HasOne("FBank.Domain.Entities.Bank", "Bank")
                        .WithMany("Agencies")
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bank");
                });

            modelBuilder.Entity("FBank.Domain.Entities.Agency", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("FBank.Domain.Entities.Bank", b =>
                {
                    b.Navigation("Agencies");
                });

            modelBuilder.Entity("FBank.Domain.Entities.Client", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
