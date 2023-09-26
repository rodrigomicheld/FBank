using FBank.Application.Interfaces;
using FBank.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FBank.Infrastructure.Repositories
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
            Context.SaveChanges();
        }

        public void Insert(T entity)
        {
            entity.CreateDateAt = DateTime.Now;
            entity.UpdateDateAt = DateTime.Now;
            Entity.Add(entity);
            Context.SaveChanges();
        }

        public T SelectToId(Guid id)
        {
            return Entity.Find(id);
        }

        public void Update(T entity)
        {
            entity.UpdateDateAt = DateTime.Now;
            Entity.Add(entity);
            Context.SaveChanges();
        }
    }
}