using Application.ViewMoldels;
using Domain.Common;
using Domain.Common.Filters;
using MediatR;

namespace Application.Queries.Accounts
{
    public class ListExtractClientQuery : IRequest<PaginationResponse<ClientExtractViewModel>>
    {
        public FilterClient FilterClient { get; set; }
    }
}
