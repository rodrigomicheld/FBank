using Application.Interfaces;
using Application.Requests.Login;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Login
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
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(TokenRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Autenticando cliente: {request.Document}");

            var client = _unitOfWork.ClientRepository.SelectOne(
                         x => x.Accounts.Any(account => account.Client.Document == request.Document) &&
                         x.Password == request.Password);

            if (client == null)
                throw new ArgumentException("Incorrect account or password!");

            return _tokenService.GerarToken(client);
        }
    }
}
