using FBank.Application.ViewMoldels;
using FBank.Domain.Common;
using FBank.Domain.Common.Filters;
using MediatR;

namespace FBank.Application.Queries
{
    public class ListExtractClientQuery : IRequest<PaginationResponse<ClientExtractViewModel>>
    {
        public FilterClient FilterClient { get; set; }
    }
}
