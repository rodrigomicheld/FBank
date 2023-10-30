using FBank.Application.ViewMoldels;
using MediatR;

namespace FBank.Application.Queries.Accounts
{
    public class GetOneClientQuery : IRequest<ClientViewModel>
    {
        public string Document { get; set; }
    }
}
