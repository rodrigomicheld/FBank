using AutoMapper;
using FBank.Application.Interfaces;
using FBank.Application.Requests;
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
        private readonly ITransactionRepository _transactionRepository;
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
            _transactionRepository = iTransactionRepository;
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
                _logger.LogError($"Deposito na Agência/conta: {request.Agency}/{request.Account} não realizado. Valor informado inválido {request.Value} ");
                throw new InvalidOperationException($"Valor Informado inválido: R${request.Value}");
            }

            var transactionBank = CompleteDataDeposit(request);

            _transactionRepository.Insert(transactionBank);

            var transactionReturn = _transactionRepository.SelectToId(transactionBank.Id);

            await _mediator.Send(new UpdateBalanceAccountRequest()
            {
                AccountId = (Guid)transactionBank.AccountId,
                Value = transactionBank.Value,
                FlowType = transactionBank.FlowType
            });
            
            var mappedResult = _mapper.Map<TransactionViewModel>(transactionReturn);
            
            return await Task.FromResult(mappedResult);
        }

        public Transaction CompleteDataDeposit(DepositMoneyAccountRequest request)
        {
            var account = VerifyAccountExists(request.Account, request.Agency);

            return new Transaction()
            {
                TransactionType = TransactionType.DEPOSIT,
                FlowType = FlowType.INPUT,
                AccountToId = null,
                AccountId = account.Id,
                Value = request.Value
            };
        }

        public bool VerifyValueDeposit(decimal valueDeposit)
        {
            if (valueDeposit > 0)
                return true;
            else
                return false;
        }
        public Account VerifyAccountExists(int accountNumber, int agency)
        {
            var account = _accountRepository.SelectOne(x => x.Number == accountNumber && x.Agency.Code == agency);

            if (account == null)
                throw new Exception($"Conta não encontrada");
            return account;
        }
    }
}
