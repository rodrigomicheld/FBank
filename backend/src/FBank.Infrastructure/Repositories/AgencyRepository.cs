using FBank.Application.Interfaces;
using FBank.Domain.Entities;

namespace FBank.Infrastructure.Repositories
{
    public class AgencyRepository : BaseRepository<Agency>, IAgencyRepository
    {
        public AgencyRepository(DataBaseContext context) : base(context)
        {
        }
    }
}
