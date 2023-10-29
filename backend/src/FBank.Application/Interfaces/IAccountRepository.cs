using FBank.Domain.Entities;

namespace FBank.Application.Interfaces
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        int? SelectNumberMax();
    }
}
