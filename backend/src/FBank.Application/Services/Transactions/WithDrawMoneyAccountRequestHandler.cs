using FBank.Application.Interfaces;
using FBank.Application.Requests.Transactions;
using FBank.Application.ViewMoldels;
using FBank.Domain.Entities;
using FBank.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FBank.Application.Services.Transactions
{
    public class WithDrawMoneyAccountRequestHandler : IRequestHandler<WithDrawMoneyAccountRequest, TransactionViewModel>
    {
        IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly ILogger<WithDrawMoneyAccountRequestHandler> _logger;
        public WithDrawMoneyAccountRequestHandler(IUnitOfWork unitOfWork, IMediator mediator, ILogger<WithDrawMoneyAccountRequestHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<TransactionViewModel> Handle(WithDrawMoneyAccountRequest request, CancellationToken cancellationToken)
        {

                _logger.LogInformation($"Sacando valor {request.Amount} da conta: {request.AccountNumber}");

            decimal amount = request.Amount;
            var account = _unitOfWork.AccountRepository.SelectOne(x => x.Number == request.AccountNumber);
            if (account == null)
                throw new InvalidOperationException("Account not found!");
            if (account.Status == AccountStatus.Inactive)
                throw new InvalidOperationException($"Account is Inactive!");
            try
            {
                await _mediator.Send(new UpdateBalanceAccountRequest()
                {
                    AccountId = account.Id,
                    Value = amount,
                    FlowType = FlowType.OUTPUT
                });
            }
            catch (Exception)
            {
                throw;
            }

            var transfer = new Transaction
            {
                Account = account,
                AccountToId = account.Id,
                AccountId = account.Id,
                FlowType = FlowType.OUTPUT,
                TransactionType = TransactionType.WITHDRAW,
                Value = amount,
                CreateDateAt = DateTime.UtcNow,
                UpdateDateAt = DateTime.UtcNow,
            };

            _unitOfWork.TransactionRepository.Insert(transfer);

            return new TransactionViewModel
            {
                Amount = amount,
                DateTransaction = transfer.CreateDateAt,
                TransactionType = Domain.Enums.TransactionType.WITHDRAW,
            };            
        }
    }
}
