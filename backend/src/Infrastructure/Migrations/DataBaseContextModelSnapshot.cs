﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    partial class DataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("AgencyId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("agencia_id");

                    b.Property<decimal>("Balance")
                        .HasColumnType("Decimal(21,2)")
                        .HasColumnName("saldo");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("cliente_id");

                    b.Property<DateTime>("CreateDateAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("criado_em");

                    b.Property<int>("Number")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("numero");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Number"));

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.Property<DateTime>("UpdateDateAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("atualizado_em");

                    b.HasKey("Id");

                    b.HasAlternateKey("Number");

                    b.HasIndex("AgencyId");

                    b.HasIndex("ClientId");

                    b.ToTable("Conta", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Agency", b =>
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

            modelBuilder.Entity("Domain.Entities.Bank", b =>
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

            modelBuilder.Entity("Domain.Entities.Client", b =>
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

                    b.Property<int>("DocumentType")
                        .HasColumnType("int")
                        .HasColumnName("tipo_documento");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("nome");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("senha");

                    b.Property<DateTime>("UpdateDateAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("atualizado_em");

                    b.HasKey("Id");

                    b.HasAlternateKey("Document");

                    b.ToTable("Cliente", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("conta_id");

                    b.Property<Guid?>("AccountToId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("conta_destino_id");

                    b.Property<DateTime>("CreateDateAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("criado_em");

                    b.Property<int>("FlowType")
                        .HasColumnType("int")
                        .HasColumnName("fluxo");

                    b.Property<int>("TransactionType")
                        .HasColumnType("int")
                        .HasColumnName("tipo_transacao");

                    b.Property<DateTime>("UpdateDateAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("atualizado_em");

                    b.Property<decimal>("Value")
                        .HasColumnType("Decimal(21,2)")
                        .HasColumnName("valor");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Transacao", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Account", b =>
                {
                    b.HasOne("Domain.Entities.Agency", "Agency")
                        .WithMany("Accounts")
                        .HasForeignKey("AgencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Client", "Client")
                        .WithMany("Accounts")
                        .HasForeignKey("ClientId");

                    b.Navigation("Agency");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Domain.Entities.Agency", b =>
                {
                    b.HasOne("Domain.Entities.Bank", "Bank")
                        .WithMany("Agencies")
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bank");
                });

            modelBuilder.Entity("Domain.Entities.Transaction", b =>
                {
                    b.HasOne("Domain.Entities.Account", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Domain.Entities.Account", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Domain.Entities.Agency", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("Domain.Entities.Bank", b =>
                {
                    b.Navigation("Agencies");
                });

            modelBuilder.Entity("Domain.Entities.Client", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}