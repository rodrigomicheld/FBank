using FBank.Application.Interfaces;
using FBank.Application.Requests;
using FBank.Application.ViewMoldels;
using FBank.Domain.Entities;
using FBank.Domain.Enums;
using MediatR;

namespace FBank.Application.Services
{
    public class TransferMoneyAccountRequestHandler : IRequestHandler<TransferMoneyAccountRequest, TransferViewModel>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        protected IMediator _mediator;
        public TransferMoneyAccountRequestHandler(ITransactionRepository transactionRepository,
            IAccountRepository accountRepository,
            IMediator mediator)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _mediator = mediator;
        }

        public async Task<TransferViewModel> Handle(TransferMoneyAccountRequest request, CancellationToken cancellationToken)
        {
            List<string> errors = new List<string>();
            var accountFrom = _accountRepository.SelectOne(x => x.Number == request.AccountNumberFrom);
            var accountTo = _accountRepository.SelectOne(x => x.Number == request.AccountNumberTo);
            if (accountFrom == null)
                errors.Add("Conta Origem Não encontrada");
            if (accountTo == null)
                errors.Add("Conta Destino Não encontrada");
            if (request.Value <= 0)
                errors.Add("Valor da transferência não pode menor ou igual a zero");


            if (errors.Count > 0)
                throw new Exception($"Erro ao Realizar Transferência, erros : {String.Join(",",errors)}");
            if ((accountFrom.Balance - request.Value) < 0)
                throw new Exception($"Saldo insulficiente para realizar a trasferência, saldo atual {accountFrom.Balance}");


           Transaction transactionFrom = new Transaction()
            {
             AccountToId = accountTo.Id,
             TransactionType = TransactionType.TRANSFER,
             Value = request.Value,
             FlowType = FlowType.OUTPUT,
             AccountId = accountFrom.Id
            };

            _transactionRepository.Insert(transactionFrom);
            //update balance of AccountFrom
            await _mediator.Send(new UpdateBalanceAccountRequest() { AccountId = accountFrom.Id,
                                                                Value = transactionFrom.Value,
                                                                FlowType = transactionFrom.FlowType
                                                                });

            Transaction transactionTo = new Transaction()
            {
                AccountToId = accountFrom.Id,
                TransactionType = TransactionType.TRANSFER,
                Value = request.Value,
                FlowType = FlowType.INPUT,
                AccountId = accountTo.Id
            };

           
            _transactionRepository.Insert(transactionTo);
            //update balance of to
            await _mediator.Send(new UpdateBalanceAccountRequest()
            {
                AccountId = accountTo.Id,
                Value = transactionTo.Value,
                FlowType = transactionTo.FlowType
            });
            return new TransferViewModel();            
        }
    }
}
