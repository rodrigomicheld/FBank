using FBank.Application.Interfaces;
using FBank.Application.Requests.Transactions;
using FBank.Domain.Entities;
using FBank.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FBank.Application.Services.Transactions
{
    public class TransferMoneyAccountRequestHandler : IRequestHandler<TransferMoneyAccountRequest, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        protected IMediator _mediator;
        private readonly ILogger _logger;

        public TransferMoneyAccountRequestHandler(IMediator mediator,
            IUnitOfWork unitOfWork,
            ILogger<TransferMoneyAccountRequestHandler> logger)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<string> Handle(TransferMoneyAccountRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Transferindo valor {request.Value} da conta: {request.AccountNumberFrom}");

            List<string> errors = new List<string>();
            var accountFrom = _unitOfWork.AccountRepository.SelectOne(x => x.Number == request.AccountNumberFrom);
            var accountTo = _unitOfWork.AccountRepository.SelectOne(x => x.Number == request.AccountNumberTo);
            if (accountFrom == null)
                errors.Add("Account not found");
            if (accountTo == null)
                errors.Add("Destination account not found");
            if (accountTo == accountFrom)
                errors.Add("Transfer to the same account is not permitted");
            if (request.Value <= 0)
                errors.Add("Transfer amount cannot be less than or equal to zero");
            if (accountTo.Status == AccountStatus.Inactive)
                errors.Add($"Account to is Inactive");
            if (accountFrom.Status == AccountStatus.Inactive)
                errors.Add($"Account from is Inactive");

            if (errors.Count > 0)
                throw new Exception($"Error Performing Transfer, errors : {string.Join(",", errors)}");
            if (accountFrom.Balance - request.Value < 0)
                throw new Exception($"Insufficient balance to make the transfer, current balance {accountFrom.Balance}");


            Transaction transactionFrom = new Transaction()
            {
                AccountToId = accountTo.Id,
                TransactionType = TransactionType.TRANSFER,
                Value = request.Value,
                FlowType = FlowType.OUTPUT,
                AccountId = accountFrom.Id
            };

            _unitOfWork.TransactionRepository.Insert(transactionFrom);
            try
            {
                await _mediator.Send(new UpdateBalanceAccountRequest()
                {
                    AccountId = accountFrom.Id,
                    Value = transactionFrom.Value,
                    FlowType = transactionFrom.FlowType
                });
            }
            catch (Exception)
            {
                throw;
            }

            Transaction transactionTo = new Transaction()
            {
                AccountToId = accountTo.Id,
                TransactionType = TransactionType.TRANSFER,
                Value = request.Value,
                FlowType = FlowType.INPUT,
                AccountId = accountTo.Id
            };


            _unitOfWork.TransactionRepository.Insert(transactionTo);

            try
            {
                await _mediator.Send(new UpdateBalanceAccountRequest()
                {
                    AccountId = accountTo.Id,
                    Value = transactionTo.Value,
                    FlowType = transactionTo.FlowType
                });
            }
            catch (Exception)
            {
                throw;
            }

            return "Successful transfer";
        }
    }
}
