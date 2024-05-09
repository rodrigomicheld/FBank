using Application.ViewMoldels;
using MediatR;

namespace Application.Queries.Accounts
{
    public class GetOneClientQuery : IRequest<ClientViewModel>
    {
        public string Document { get; set; }
    }
}
