using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(DataBaseContext context) : base(context) {}

        public override Client SelectOne(Expression<Func<Client, bool>> filter = null)
        {
            IQueryable<Client> query = context.Clients;
    
            query = query.Include(client => client.Accounts)
                .ThenInclude(account => account.Agency);

            return query.Where(filter).FirstOrDefault();        
        }
    }
}
