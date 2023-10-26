using FBank.Application.Interfaces;
using FBank.Application.Requests;
using FBank.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FBank.Application.Services
{
    public class TokenRequestHandler : IRequestHandler<TokenRequest, string>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<TokenRequestHandler> _logger;
        private readonly ITokenService _tokenService;

        public TokenRequestHandler(IClientRepository clientRepository, ILogger<TokenRequestHandler> logger, ITokenService tokenService)
        {
            _clientRepository = clientRepository;
            _logger = logger;
            _tokenService = tokenService;
        }

        public async Task<string> Handle(TokenRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Autenticando cliente da conta: {request.NumberAccount}");

            var client = _clientRepository.SelectOne(
                         x => x.Accounts.Any(account => account.Status == AccountStatus.Active && 
                         account.Number == request.NumberAccount && account.Agency.Code == request.NumberAgency) && 
                         x.Password == request.Password);

            if (client == null)
                throw new ArgumentException("Incorrect account or password!");

            return _tokenService.GerarToken(client);
        }
    }
}
