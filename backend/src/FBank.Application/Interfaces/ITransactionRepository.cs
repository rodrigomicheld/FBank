using FBank.Application.Dto;
using FBank.Domain.Common;
using FBank.Domain.Common.Filters;
using FBank.Domain.Entities;

namespace FBank.Application.Interfaces
{
    public interface ITransactionRepository : IBaseRepository<TransactionBank>
    {
        Task<PaginationResponse<ClientExtractToListDto>> SelectManyWithFilterToList(FilterClient filterClient);
    }
}
