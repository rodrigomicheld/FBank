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
            //Todo: Trocar esta verificação, por uma rotina de validação, quando a rotina de verifica se uma conta existe estiver pronta
            //Todo: Incluir método para verificar se a conta existe
            if (!VerifyValueDeposit(request.Value)) 
            {
                var transactionViewModel = new TransactionViewModel();
                transactionViewModel.Id = Guid.Empty;
                return Task.FromResult(transactionViewModel);
            }

            var transactionBank = CompleteDataDeposit(request);
            _iTransactionRepository.Insert(transactionBank);
            var transactionReturn = _iTransactionRepository.SelectToId(transactionBank.Id);
            //Todo: Incluir método para atualizar Saldo, quando a rotina de saldo estiver pronta
            var mappedResult = _mapper.Map<TransactionViewModel>(transactionReturn);
            return Task.FromResult(mappedResult);
        }        

        public TransactionBank CompleteDataDeposit(DepositMoneyAccountRequest request) 
        {
            var transactionBank = new TransactionBank();
            transactionBank.TransactionType = Domain.Enums.TransactionType.DEPOSITO;
            transactionBank.FlowType = Domain.Enums.FlowType.ENTRADA;
            transactionBank.AccountFromId = Guid.Empty;
            transactionBank.AccountToId= request.AccountToId;
            transactionBank.Value= request.Value;       
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
