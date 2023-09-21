using FBank.Application.Interfaces;
using FBank.Domain.Entities;

namespace FBank.Infrastructure.Repositories
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(DataBaseContext context) : base(context)
        {
        }
    }
}
