using FBank.Application.Interfaces;
using FBank.Application.Requests;
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
            _logger.LogInformation($"Autenticando cliente: {request.Document}");

            var client = _clientRepository.SelectOne(x => x.Document == request.Document && x.Password == request.Password);

            if (client == null)
                throw new ArgumentException("Incorrect client or password!");

            return _tokenService.GerarToken(client);
        }
    }
}
