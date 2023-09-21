using FBank.Domain.Common;

namespace FBank.Application.Common.Interfaces
{
    public interface IBaseRepository<T> where T : EntityBase
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        T SelectPerId(Guid id);
    }
}