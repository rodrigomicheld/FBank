using FBank.Application.Interfaces;
using FBank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FBank.Infrastructure.Repositories
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(DataBaseContext context) : base(context) {}

        public override Client SelectOne(Expression<Func<Client, bool>> filter = null)
        {
            IQueryable<Client> query = Context.Clients;
    
            query = query.Include(client => client.Accounts);

            return query.AsNoTracking()
                .Where(filter).FirstOrDefault();        
        }
    }
}
