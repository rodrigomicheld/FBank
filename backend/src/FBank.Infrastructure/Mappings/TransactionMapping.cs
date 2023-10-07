using FBank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FBank.Infrastructure.Mappings
{
    public class TransactionMapping : BaseMapping<TransactionBank>
    {
        protected override void MapEntity(EntityTypeBuilder<TransactionBank> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Transacao");

            entityTypeBuilder.Property(p => p.TransactionType)
                .IsRequired()
                .HasColumnName("tipo_transacao");

            entityTypeBuilder.Property(p => p.Value)
               .IsRequired()
               .HasColumnType("Decimal(21,2)")
               .HasColumnName("valor");

            entityTypeBuilder.Property(p => p.AccountToId)
                .IsRequired()
                .HasColumnName("conta_destino_id");

            entityTypeBuilder.HasOne(p => p.AccountTo)
             .WithMany(a => a.Transactions);

            entityTypeBuilder.Property(p => p.AccountFromId)
                .IsRequired()
                .HasColumnName("conta_origem_id");
        }
    }
}