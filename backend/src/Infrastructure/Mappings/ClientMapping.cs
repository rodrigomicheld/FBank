﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings
{
    public class ClientMapping : BaseMapping<Client>
    {
        protected override void MapEntity(EntityTypeBuilder<Client> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Cliente");

            entityTypeBuilder.Property(p => p.Name)
                .IsRequired()
                .HasColumnName("nome");

            entityTypeBuilder.Property(p => p.Document)
                .IsRequired()
                .HasColumnName("documento")
                .HasMaxLength(20);

            entityTypeBuilder.Property(p => p.DocumentType)
                .IsRequired()
                .HasColumnName("tipo_documento");

            entityTypeBuilder.Property(p => p.Password)
                .IsRequired()
                .HasColumnName("senha")
                .HasMaxLength(50);

            entityTypeBuilder.HasAlternateKey(p => p.Document);    


            entityTypeBuilder.HasMany(x => x.Accounts)
                .WithOne(a => a.Client)
                .HasForeignKey(c => c.ClientId)
                .IsRequired(false);
        }
    }
}
