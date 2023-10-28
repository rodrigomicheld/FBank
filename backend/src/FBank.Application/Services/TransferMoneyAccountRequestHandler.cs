using FBank.Application.Interfaces;
using FBank.Application.Requests;
using FBank.Application.Requests.Transactions;
using FBank.Application.ViewMoldels;
using FBank.Domain.Entities;
using FBank.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FBank.Application.Services
{
    public class TransferMoneyAccountRequestHandler : IRequestHandler<TransferMoneyAccountRequest, TransferViewModel>
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
        public async Task<TransferViewModel> Handle(TransferMoneyAccountRequest request, CancellationToken cancellationToken)
        {
            try
            {
                List<string> errors = new List<string>();
                var accountFrom = _unitOfWork.AccountRepository.SelectOne(x => x.Number == request.AccountNumberFrom);
                var accountTo = _unitOfWork.AccountRepository.SelectOne(x => x.Number == request.AccountNumberTo);
                if (accountFrom == null)
                    errors.Add("Account not found");
                if (accountTo == null)
                    errors.Add("Destination account not found");
                if (request.Value <= 0)
                    errors.Add("Transfer amount cannot be less than or equal to zero");


                if (errors.Count > 0)
                    throw new Exception($"Error Performing Transfer, errors : {String.Join(",", errors)}");
                if ((accountFrom.Balance - request.Value) < 0)
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

                await _mediator.Send(new UpdateBalanceAccountRequest()
                {
                    AccountId = accountFrom.Id,
                    Value = transactionFrom.Value,
                    FlowType = transactionFrom.FlowType
                });

                Transaction transactionTo = new Transaction()
                {
                    AccountToId = accountTo.Id,
                    TransactionType = TransactionType.TRANSFER,
                    Value = request.Value,
                    FlowType = FlowType.INPUT,
                    AccountId = accountTo.Id
                };


                _unitOfWork.TransactionRepository.Insert(transactionTo);

                await _mediator.Send(new UpdateBalanceAccountRequest()
                {
                    AccountId = accountTo.Id,
                    Value = transactionTo.Value,
                    FlowType = transactionTo.FlowType
                });
                _unitOfWork.Commit();
                return new TransferViewModel();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.LogInformation(ex.ToString());
                throw;
            }
        }

    }
}
