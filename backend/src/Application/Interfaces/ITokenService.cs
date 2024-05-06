using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITokenService
    {
        string GerarToken(Client client);
    }
}
