using FBank.Application.Interfaces;
using FBank.Domain.Entities;

namespace FBank.Infrastructure.Repositories
{
    public class BankRepository : BaseRepository<Bank>, IBankRepository
    {
        public BankRepository(DataBaseContext context) : base(context)
        {
        }
    }
}
