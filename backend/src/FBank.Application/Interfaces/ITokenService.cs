using FBank.Domain.Entities;

namespace FBank.Application.Interfaces
{
    public interface ITokenService
    {
        string GerarToken(Client client);
    }
}
