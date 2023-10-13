using MediatR;

namespace FBank.Application.Queries
{
    public class PostOneClientQuery : IRequest<int>
    {
        public string Name { get; set; }
        public string Document { get; set; }
    }
}
