using AutoMapper;
using FBank.Application.Interfaces;
using FBank.Application.Requests;
using FBank.Application.ViewMoldels;
using FBank.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FBank.Application.Services
{
    public class DepositMoneyAccountHandler : IRequestHandler<DepositMoneyAccountRequest, TransactionViewModel>
    {

        private readonly ITransactionRepository _iTransactionRepository;
        private readonly ILogger<DepositMoneyAccountHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public DepositMoneyAccountHandler(
            IMediator mediator,
            ITransactionRepository iTransactionRepository,
                ILogger<DepositMoneyAccountHandler> logger,
                IMapper mapper)
        {
            _mediator = mediator;
            _iTransactionRepository = iTransactionRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public Task<TransactionViewModel> Handle(DepositMoneyAccountRequest request, CancellationToken cancellationToken)
        {
            var transactionBank = CompleteDataDeposit(request);
            _iTransactionRepository.Insert(transactionBank);
            var transactionReturn = _iTransactionRepository.SelectToId(transactionBank.Id);
            var mappedResult = _mapper.Map<TransactionViewModel>(transactionReturn);
            return Task.FromResult(mappedResult);
        }

        private TransactionBank CompleteDataDeposit(DepositMoneyAccountRequest request) 
        {
            var transactionBank = new TransactionBank();
            transactionBank.TransactionType = Domain.Enums.TransactionType.DEPOSITO;
            transactionBank.FlowType = Domain.Enums.FlowType.ENTRADA;
            transactionBank.AccountFromId = Guid.Empty;
            transactionBank.AccountToId= request.AccountToId;
            transactionBank.Value= request.Value;       
            return transactionBank;
        }
    }
}
