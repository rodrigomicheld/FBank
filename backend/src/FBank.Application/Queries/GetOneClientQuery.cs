using FBank.Application.ViewMoldels;
using MediatR;

namespace FBank.Application.Queries
{
    public class GetOneClientQuery : IRequest<ClientViewModel>
    {
        public Guid Id { get; set; }
    }
}
