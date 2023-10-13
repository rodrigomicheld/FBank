using FBank.Domain.Entities;
using System.Linq.Expressions;

namespace FBank.Application.Interfaces
{
    public interface IBaseRepository<T> where T : EntityBase
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        T SelectToId(Guid id);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate = null);
        T SelectOne(Expression<Func<T, bool>> filter = null);
    }
}