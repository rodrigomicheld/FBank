using FBank.Application.Dto;
using FBank.Application.Interfaces;
using FBank.Domain.Common;
using FBank.Domain.Common.Filters;
using FBank.Domain.Entities;
using FBank.Infrastructure.Common;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace FBank.Infrastructure.Repositories
{
    public class TransactionRepository : BaseRepository<TransactionBank>, ITransactionRepository
    {
        public TransactionRepository(DataBaseContext context) : base(context)
        {
        }

        public async Task<PaginationResponse<ClientExtractToListDto>> SelectManyWithFilterToList(FilterClient filterClient)
        {
            string[] sort = null;
            if (filterClient._order is not null)
                sort = filterClient._order.Split(",");

            var query = GetExtractClientWithFilter(filterClient);

            return await Entity
                .Where(query)
                .Select(x => new ClientExtractToListDto
                {
                    IdTransaction = x.Id,
                    DateTransaction = x.CreateDateAt,
                    Amount = x.Value,
                    TransactionType = x.TransactionType,
                    IdAccountOrigin = x.AccountId,
                    IdAccountDestination = x.AccountToId
                })
                .AsNoTracking()
                .Sort(sort)
                .PaginateAsync(filterClient._page, filterClient._size);
        }

        private ExpressionStarter<TransactionBank> GetExtractClientWithFilter(FilterClient filterClient)
        {
            var predicate = PredicateBuilder.New<TransactionBank>(defaultExpression: true);

            predicate = predicate.And(x => x.CreateDateAt == filterClient.InitialDate && x.CreateDateAt <= filterClient.FinalDate);

            if(filterClient.FlowType is not null)
                predicate = predicate.And(x => x.FlowType.Equals(filterClient.FlowType));
            

            return predicate;
        }
    }
}
