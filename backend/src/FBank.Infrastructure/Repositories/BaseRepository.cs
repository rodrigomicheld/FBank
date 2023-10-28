using FBank.Application.Interfaces;
using FBank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FBank.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : EntityBase
    {
        protected readonly DataBaseContext context;
        protected readonly DbSet<T> dbSet;

        protected BaseRepository(DataBaseContext context)
        {
            this.context = context;
            dbSet = this.context.Set<T>();
        }

        public void Delete(T entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            
            dbSet.Remove(entity);
        }

        public void Insert(T entity)
        {
            entity.CreateDateAt = DateTime.Now;
            entity.UpdateDateAt = DateTime.Now;
            dbSet.Add(entity);
        }

        public T SelectToId(Guid id)
        {
            return dbSet.Find(id);
        }

        public void Update(T entity)
        {
            entity.UpdateDateAt = DateTime.Now;
            dbSet.Attach(entity);
            this.context.Entry(entity).State = EntityState.Modified;
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return dbSet.Where(predicate);
            }
            return dbSet.AsEnumerable();
        }

        public virtual T SelectOne(Expression<Func<T, bool>> filter = null)
        {
            return dbSet.Where(filter).FirstOrDefault();
        }
    }
}
