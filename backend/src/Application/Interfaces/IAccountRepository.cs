using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        int? SelectNumberMax();
    }
}
