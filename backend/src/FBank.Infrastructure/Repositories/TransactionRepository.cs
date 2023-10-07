using FBank.Application.Interfaces;
using FBank.Domain.Entities;

namespace FBank.Infrastructure.Repositories
{
    public class TransactionRepository : BaseRepository<TransactionBank>, ITransactionRepository
    {
        public TransactionRepository(DataBaseContext context) : base(context)
        {
        }
    }
}
