using MediatR;

namespace FBank.Application.Requests
{
    public class TokenRequest : IRequest<string>
    {
        public string Document { get; set; }
        public string Password { get; set; }
    }
}
