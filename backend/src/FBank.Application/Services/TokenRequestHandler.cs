using FBank.Application.Interfaces;
using FBank.Application.Requests;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FBank.Application.Services
{
    public class TokenRequestHandler : IRequestHandler<TokenRequest, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TokenRequestHandler> _logger;
        private readonly ITokenService _tokenService;

        public TokenRequestHandler(IUnitOfWork unitOfWork, ILogger<TokenRequestHandler> logger, ITokenService tokenService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _unitOfWork=unitOfWork;
        }

        public async Task<string> Handle(TokenRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Autenticando cliente da conta: {request.NumberAccount}");

            var client = _unitOfWork.ClientRepository.SelectOne(
                         x => x.Accounts.Any(account => account.Number == request.NumberAccount && account.Agency.Code == request.NumberAgency) && 
                         x.Password == request.Password);

            if (client == null)
                throw new ArgumentException("Incorrect account or password!");

            return _tokenService.GerarToken(client);
        }
    }
}
