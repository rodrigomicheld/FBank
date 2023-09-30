﻿using FBank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FBank.Infrastructure.Mappings
{
    public class AccountMapping : BaseMapping<Account>
    {
        protected override void MapEntity(EntityTypeBuilder<Account> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Conta");

            entityTypeBuilder.Property(p => p.IdStatus)
                .IsRequired()
                .HasColumnName("IdStatus");

            entityTypeBuilder.HasOne(p => p.Client)
                .WithOne(q => q.Account)
                .HasForeignKey<Account>(r => r.ClientId)
                .IsRequired();
            entityTypeBuilder.Property(p => p.ClientId)
             .IsRequired()
             .HasColumnName("Cliente_id");

            entityTypeBuilder.HasOne(p => p.Agency)
                .WithMany(q => q.Accounts)
                .IsRequired();
            entityTypeBuilder.Property(p => p.AgencyId)
              .IsRequired()
              .HasColumnName("Agencia_id");
        }
    }
}
