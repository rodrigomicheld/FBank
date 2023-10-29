using FBank.Application.Interfaces;
using FBank.Application.Requests.Transactions;
using FBank.Application.ViewMoldels;
using FBank.Domain.Entities;
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
            try
            {
                decimal amount = request.Amount;
                var account = _unitOfWork.AccountRepository.SelectOne(x => x.Number == request.AccountNumber);
                if (account == null)
                    throw new InvalidOperationException("Account not found!");

                await _mediator.Send(new UpdateBalanceAccountRequest()
                {
                    AccountId = account.Id,
                    Value = amount,
                    FlowType = Domain.Enums.FlowType.OUTPUT
                });

                var transfer = new Transaction
                {
                    Account = account,
                    AccountToId = account.Id,
                    AccountId = account.Id,
                    FlowType = Domain.Enums.FlowType.OUTPUT,
                    TransactionType = Domain.Enums.TransactionType.WITHDRAW,
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
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                throw;
            }
        }
    }
}
