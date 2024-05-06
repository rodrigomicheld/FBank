using Application.Interfaces;
using MediatR;

namespace Application.Requests.Login
{
    public class TokenRequest : IRequest<string>, IPersistable
    {
        public string Document { get; set; }
        public string Password { get; set; }
    }
}
