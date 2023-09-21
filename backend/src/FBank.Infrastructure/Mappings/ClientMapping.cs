﻿using FBank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FBank.Infrastructure.Mappings
{
    public class ClientMapping : BaseMapping<Client>
    {
        protected override void MapearEntidade(EntityTypeBuilder<Client> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Client");

            entityTypeBuilder.Property(p => p.Name)
                .IsRequired()
                .HasColumnName("nome");
        }
    }
}
