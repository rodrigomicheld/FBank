using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class AgencyRepository : BaseRepository<Agency>, IAgencyRepository
    {
        public AgencyRepository(DataBaseContext context) : base(context)
        {
        }
    }
}
