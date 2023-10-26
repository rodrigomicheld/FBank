using FBank.Application.Interfaces;
using FBank.Application.Requests;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FBank.Application.Services
{
    public class AccountStatusRequestHandler : IRequestHandler<AccountStatusRequest, string>
    {
        private readonly ILogger<AccountStatusRequestHandler> _logger;
        private readonly IAccountRepository _accountRepository;

        public AccountStatusRequestHandler(
            ILogger<AccountStatusRequestHandler> logger,
            IAccountRepository accountRepository)
        {
            _logger = logger;
            _accountRepository = accountRepository;
        }

        public async Task<string> Handle(AccountStatusRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Atualizando status da conta: {request.AccountNumber} - Status: {request.AccountStatus}");

            var account = _accountRepository.SelectOne(x => x.Number == request.AccountNumber);

            if (account == null)
                throw new InvalidOperationException("Account does not exist!");

            if (account.Status == request.AccountStatus)
                throw new InvalidOperationException($"Account is {request.AccountStatus}!");

            account.Status = request.AccountStatus;
            _accountRepository.Update(account);

            return await Task.FromResult($"Account {account.Status} is successfully.");
        }        
    }
}
