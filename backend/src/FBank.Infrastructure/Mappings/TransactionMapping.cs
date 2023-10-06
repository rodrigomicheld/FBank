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

            entityTypeBuilder.Property(p => p.AccountId)
                .IsRequired()
                .HasColumnName("conta_id");

            entityTypeBuilder.Property(p => p.TransactionType)
                .IsRequired()
                .HasColumnName("tipo_transacao");

            entityTypeBuilder.Property(p => p.Value)
               .IsRequired()
               .HasColumnType("Decimal(21,2)")
               .HasColumnName("valor");

            entityTypeBuilder.HasOne(p => p.AccountTo)
             .WithMany(a => a.Transactions);


            entityTypeBuilder.Property(p => p.AccountId)
             .IsRequired()
             .HasColumnName("conta_id");
            

        }
    }
}