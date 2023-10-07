using FBank.Domain.Entities;
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
                .HasColumnName("Status_Id");

            entityTypeBuilder.Property(p => p.Balance)
                .IsRequired()
                .HasColumnType("Decimal(21,9)")
                .HasColumnName("Balance");

            entityTypeBuilder.HasOne(p => p.Client)
                .WithMany(q => q.Accounts);

            entityTypeBuilder.Property(p => p.ClientId)
             .IsRequired()
             .HasColumnName("Cliente_id");

            entityTypeBuilder.HasOne(p => p.Agency)
                .WithMany(q => q.Accounts)
                .IsRequired();
            entityTypeBuilder.Property(p => p.AgencyId)
              .IsRequired()
              .HasColumnName("Agencia_id");

            entityTypeBuilder.HasMany(t => t.Transactions)
                .WithOne(p => p.AccountTo)
                .HasForeignKey(p => p.AccountId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
