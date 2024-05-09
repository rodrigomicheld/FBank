using Application.Dto;
using Domain.Common;
using Domain.Common.Filters;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        Task<PaginationResponse<ClientExtractToListDto>> SelectManyWithFilterToList(FilterClient filterClient);
    }
}
