using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class BankRepository : BaseRepository<Bank>, IBankRepository
    {
        public BankRepository(DataBaseContext context) : base(context)
        {
        }
    }
}
