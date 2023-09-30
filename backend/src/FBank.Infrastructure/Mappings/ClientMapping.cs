using FBank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FBank.Infrastructure.Mappings
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

            entityTypeBuilder.HasAlternateKey(p => p.Document);
            entityTypeBuilder.HasOne(x => x.Account)
                .WithOne(a => a.Client)
                .HasForeignKey<Account>(c => c.ClientId)
                .IsRequired(false);
        }
    }
}
