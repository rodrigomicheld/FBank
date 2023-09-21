using FBank.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FBank.Infrastructure.Common
{
    public abstract class BaseMapping<T> where T : EntityBase
    {
        public void Initialize(EntityTypeBuilder<T> builder)
        {
            MapearBase(builder);
            MapearChavePrimaria(builder);
            MapearEntidade(builder);
        }

        protected abstract void MapearEntidade(EntityTypeBuilder<T> entityTypeBuilder);

        private void MapearBase(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Id).HasColumnName("id").IsRequired();
            builder.Property(x => x.UpdateDate).HasColumnName("ultima_atualizacao").IsRequired();
           
        }

        protected virtual void MapearChavePrimaria(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}