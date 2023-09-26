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
        }
    }
}
