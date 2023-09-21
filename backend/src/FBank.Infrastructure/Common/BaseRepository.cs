using FBank.Application.Common.Interfaces;
using FBank.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace FBank.Infrastructure.Common
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : EntityBase
    {
        protected readonly DataBaseContext Context;
        protected readonly DbSet<T> Entity;

        protected BaseRepository(DataBaseContext context)
        {
            Context = context;
            Entity = Context.Set<T>();
        }

        public void Delete(T entity)
        {
            Entity.Remove(entity);
            Context.SaveChangesAsync();
        }

        public void Insert(T entity)
        {
            entity.UpdateDate = DateTime.Now;
            Entity.AddAsync(entity);
            Context.SaveChangesAsync();
        }

        public T SelectPerId(Guid id)
        {
            return Entity.Find(id);
        }

        public void Update(T entity)
        {
            entity.UpdateDate = DateTime.Now;
            Entity.AddAsync(entity);
            Context.SaveChangesAsync();
        }
    }
}