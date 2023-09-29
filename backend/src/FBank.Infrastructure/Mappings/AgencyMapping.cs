using FBank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FBank.Infrastructure.Mappings
{
    public  class AgencyMapping : BaseMapping<Agency>
    {
        protected override void MapEntity(EntityTypeBuilder<Agency> entityTypeBuilder) 
        {
            entityTypeBuilder.ToTable("Agency");

            entityTypeBuilder.Property(p => p.Id)
               .IsRequired()
               .HasColumnName("codigo");

            entityTypeBuilder.Property(p => p.BankCode)
               .IsRequired()
               .HasColumnName("codigo_banco");

            entityTypeBuilder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("nome");

            entityTypeBuilder.HasOne(a => a.Bank)
                .WithMany(b => b.Agencies);


        }
    }
}
