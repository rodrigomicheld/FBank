using MediatR;

namespace FBank.Application.Queries
{
    public class PostOneClientQuery : IRequest<string>
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public string Password { get; set; }
    }
}
