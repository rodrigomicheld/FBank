using FBank.Domain.Entities;

namespace FBank.Application.Interfaces
{
    public interface IBaseRepository<T> where T : EntityBase
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        T SelectToId(Guid id);
    }
}