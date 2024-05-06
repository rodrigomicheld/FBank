using Application.Dto;
using Application.Interfaces;
using Domain.Common;
using Domain.Common.Filters;
using Domain.Entities;
using Infrastructure.Common;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
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

            return await dbSet
                .Where(query)
                .Select(x => new ClientExtractToListDto
                {
                    IdTransaction = x.Id,
                    DateTransaction = x.CreateDateAt,
                    Amount = x.Value,
                    TransactionType = x.TransactionType,
                    IdAccountDestination = x.AccountToId ?? Guid.Empty,
                    IdAccountOrigin = x.AccountId
                    
                })
                .AsNoTracking()
                .Sort(sort)
                .PaginateAsync(filterClient._page, filterClient._size);
        }

        private ExpressionStarter<Transaction> GetExtractClientWithFilter(FilterClient filterClient)
        {
            var predicate = PredicateBuilder.New<Transaction>(defaultExpression: true);

            predicate.And(x => x.CreateDateAt >= filterClient.InitialDate && x.CreateDateAt <= filterClient.FinalDate);

            predicate.And(x => x.Account.Number == filterClient.NumberAccount);
            
            if(filterClient.FlowType is not null)
                predicate.And(x => x.FlowType.Equals(filterClient.FlowType));
            

            return predicate;
        }
    }
}
