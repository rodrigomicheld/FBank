using FBank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FBank.Infrastructure.Mappings
{
    public class BankMapping : BaseMapping<Bank>
    {
        protected override void MapEntity(EntityTypeBuilder<Bank> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Banco");

            entityTypeBuilder.Property(p => p.Code)
               .IsRequired()
               .HasColumnName("codigo_banco");

            entityTypeBuilder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("nome");

            entityTypeBuilder.HasAlternateKey(p => p.Code);
        }
    }
}
