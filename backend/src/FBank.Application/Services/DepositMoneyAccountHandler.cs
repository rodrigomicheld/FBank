using AutoMapper;
using FBank.Application.Interfaces;
using FBank.Application.Requests;
using FBank.Application.ViewMoldels;
using FBank.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Transactions;

namespace FBank.Application.Services
{
    public class DepositMoneyAccountHandler : IRequestHandler<DepositMoneyAccountRequest, TransactionViewModel>
    {
        private readonly ITransactionRepository _iTransactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<DepositMoneyAccountHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public DepositMoneyAccountHandler(
            IMediator mediator,
            ITransactionRepository iTransactionRepository,
            IAccountRepository accountRepository,
                ILogger<DepositMoneyAccountHandler> logger,
                IMapper mapper)
        {
            _mediator = mediator;
            _iTransactionRepository = iTransactionRepository;
            _logger = logger;
            _mapper = mapper;
            _accountRepository = accountRepository;
        }

        public DepositMoneyAccountHandler(IMediator mockMediator, ITransactionRepository mockTransactionRepository, ILogger<DepositMoneyAccountHandler> logger, IMapper mapper)
        {
        }

        public async Task<TransactionViewModel> Handle(DepositMoneyAccountRequest request, CancellationToken cancellationToken)
        {
            //Todo: Trocar esta verificação, por uma rotina de validação, quando a rotina de verifica se uma conta existe estiver pronta
            //Todo: Incluir método para verificar se a conta existe
            if (!VerifyValueDeposit(request.Value)) 
            {
                var transactionViewModel = new TransactionViewModel();
                transactionViewModel.Id = Guid.Empty;
                return await Task.FromResult(transactionViewModel);
            }

            var transactionBank = CompleteDataDeposit(request);
            _iTransactionRepository.Insert(transactionBank);
            var transactionReturn = _iTransactionRepository.SelectToId(transactionBank.Id);
          
            await _mediator.Send(new UpdateBalanceAccountRequest()
            {
                AccountId = transactionBank.AccountId,
                Value = transactionBank.Value,
                FlowType = transactionBank.FlowType
            });
            var mappedResult = _mapper.Map<TransactionViewModel>(transactionReturn);
            return await Task.FromResult(mappedResult);
        }        

        public TransactionBank CompleteDataDeposit(DepositMoneyAccountRequest request) 
        {
            var account = VerifyAccountExists(request.AccountNumber);
            var transactionBank = new TransactionBank();
            transactionBank.TransactionType = Domain.Enums.TransactionType.DEPOSITO;
            transactionBank.FlowType = Domain.Enums.FlowType.ENTRADA;
            transactionBank.AccountFromId = Guid.Empty;
            transactionBank.AccountToId= account.Id;
            transactionBank.AccountId = account.Id;
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
        public Account VerifyAccountExists(int accountNumber)
        {
            var account = _accountRepository.SelectOne(x => x.Number == accountNumber);
            if (account == null)
                throw new Exception($"Conta não encontrada");
            return account;
        }
    }
}
