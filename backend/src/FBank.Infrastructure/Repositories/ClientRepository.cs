using FBank.Application.Interfaces;
using FBank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FBank.Infrastructure.Repositories
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(DataBaseContext context) : base(context)
        {

        }

        public override Client SelectOne(Expression<Func<Client, bool>> filtro = null)
        {
            return Entity.Include(x=> x.Accounts).Where(filtro).FirstOrDefault();
        }
    }
}
