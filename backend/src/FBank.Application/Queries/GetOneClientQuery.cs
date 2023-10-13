using FBank.Application.ViewMoldels;
using MediatR;

namespace FBank.Application.Queries
{
    public class GetOneClientQuery : IRequest<ClientViewModel>
    {
        public string Document { get; set; }
    }
}
