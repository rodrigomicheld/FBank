using FBank.Domain.Entities;
using System.Linq.Expressions;

namespace FBank.Application.Interfaces
{
    public interface IClientRepository : IBaseRepository<Client>
    {
        Client SelectOne(Expression<Func<Client, bool>> filtro = null);
    }
}
