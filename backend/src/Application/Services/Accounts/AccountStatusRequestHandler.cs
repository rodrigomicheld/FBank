using Application.Interfaces;
using Application.Requests.Accounts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Accounts
{
    public class AccountStatusRequestHandler : IRequestHandler<AccountStatusRequest, string>
    {
        private readonly ILogger<AccountStatusRequestHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public AccountStatusRequestHandler(
            ILogger<AccountStatusRequestHandler> logger,
             IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(AccountStatusRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Atualizando status da conta: {request.AccountNumber} - Status: {request.AccountStatus}");

            var account = _unitOfWork.AccountRepository.SelectOne(x => x.Number == request.AccountNumber);

            if (account == null)
                throw new InvalidOperationException("Account does not exist!");

            if (account.Status == request.AccountStatus)
                throw new InvalidOperationException($"Account is {request.AccountStatus}!");

            account.Status = request.AccountStatus;
            _unitOfWork.AccountRepository.Update(account);

            return await Task.FromResult($"Account {account.Status} is successfully.");
        }
    }
}
