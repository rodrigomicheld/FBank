using AutoMapper;
using FBank.Application.Interfaces;
using FBank.Application.Requests.Transactions;
using FBank.Application.ViewMoldels;
using FBank.Domain.Entities;
using FBank.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FBank.Application.Services.Transactions
{
    public class DepositMoneyAccountHandler : IRequestHandler<DepositMoneyAccountRequest, TransactionViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DepositMoneyAccountHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public DepositMoneyAccountHandler(
            IMediator mediator,
            ILogger<DepositMoneyAccountHandler> logger,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<TransactionViewModel> Handle(DepositMoneyAccountRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Depositando valor {request.Value} na conta: {request.AccountNumber}");

            if (!VerifyValueDeposit(request.Value))
            {
                var transactionViewModel = new TransactionViewModel();
                return await Task.FromResult(transactionViewModel);
            }

            var account = _unitOfWork.AccountRepository.SelectOne(x => x.Number == request.AccountNumber);

            if (account == null)
                throw new InvalidOperationException($"Account not found");

            if (account.Status == AccountStatus.Inactive)
                throw new InvalidOperationException($"Account is Inactive!");

            var transactionBank = CompleteDataDeposit(request, account);
            _unitOfWork.TransactionRepository.Insert(transactionBank);
            var transactionReturn = _unitOfWork.TransactionRepository.SelectToId(transactionBank.Id);

            await _mediator.Send(new UpdateBalanceAccountRequest()
            {
                AccountId = transactionBank.AccountId,
                Value = transactionBank.Value,
                FlowType = transactionBank.FlowType
            });

            var mappedResult = _mapper.Map<TransactionViewModel>(transactionReturn);
            return await Task.FromResult(mappedResult);
        }

        public Transaction CompleteDataDeposit(DepositMoneyAccountRequest request, Account account)
        {
            var transactionBank = new Transaction();
            transactionBank.TransactionType = TransactionType.DEPOSIT;
            transactionBank.FlowType = FlowType.INPUT;
            transactionBank.AccountToId = account.Id;
            transactionBank.AccountId = account.Id;
            transactionBank.Value = request.Value;
            return transactionBank;
        }

        public bool VerifyValueDeposit(decimal valueDeposit)
        {
            if (valueDeposit > 0)
                return true;
            else
                return false;
        }
    }
}
