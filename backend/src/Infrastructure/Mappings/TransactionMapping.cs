using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings
{
    public class TransactionMapping : BaseMapping<Transaction>
    {
        protected override void MapEntity(EntityTypeBuilder<Transaction> entityTypeBuilder)
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
                .HasColumnName("conta_destino_id");

            entityTypeBuilder.HasOne(p => p.Account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(p => p.AccountId);

            entityTypeBuilder.Property(p => p.AccountId)
                .IsRequired()
                .HasColumnName("conta_id");

            entityTypeBuilder.Property(p => p.FlowType)
                .IsRequired()
                .HasColumnType("int")
                .HasColumnName("fluxo");
        }
    }
}