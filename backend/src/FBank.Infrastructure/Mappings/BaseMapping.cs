using FBank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FBank.Infrastructure.Mappings
{
    public abstract class BaseMapping<T> where T : EntityBase
    {
        public void Initialize(EntityTypeBuilder<T> builder)
        {
            MapearBase(builder);
            MapearChavePrimaria(builder);
            MapEntity(builder);
        }

        protected abstract void MapEntity(EntityTypeBuilder<T> entityTypeBuilder);

        private void MapearBase(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Id).HasColumnName("id").IsRequired();
            builder.Property(x => x.CreateDateAt).HasColumnName("criado_em").IsRequired();
            builder.Property(x => x.UpdateDateAt).HasColumnName("atualizado_em").IsRequired();
        }

        protected virtual void MapearChavePrimaria(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}