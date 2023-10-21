using FBank.Application.Interfaces;
using FBank.Application.Requests;
using FBank.Application.ViewMoldels;
using FBank.Domain.Entities;
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
            _unitOfWork=unitOfWork;
            _logger=logger;
        }
        public async Task<TransferViewModel> Handle(TransferMoneyAccountRequest request, CancellationToken cancellationToken)
        {
            try
            {
                List<string> errors = new List<string>();
                var accountFrom = _unitOfWork.AccountRepository.SelectOne(x => x.Number == request.AccountNumberFrom);
                var accountTo = _unitOfWork.AccountRepository.SelectOne(x => x.Number == request.AccountNumberTo);
                if (accountFrom == null)
                    errors.Add("Conta Origem Não encontrada");
                if (accountTo == null)
                    errors.Add("Conta Destino Não encontrada");
                if (request.Value <= 0)
                    errors.Add("Valor da transferência não pode menor ou igual a zero");


                if (errors.Count > 0)
                    throw new Exception($"Erro ao Realizar Transferência, erros : {String.Join(",", errors)}");
                if ((accountFrom.Balance - request.Value) < 0)
                    throw new Exception($"Saldo insulficiente para realizar a trasferência, saldo atual {accountFrom.Balance}");


                TransactionBank transactionFrom = new TransactionBank()
                {
                    AccountFromId = accountFrom.Id,
                    AccountToId = accountTo.Id,
                    TransactionType = Domain.Enums.TransactionType.TRANSFERENCIA,
                    Value = request.Value,
                    FlowType = Domain.Enums.FlowType.SAIDA,
                    AccountId = accountFrom.Id
                };

                _unitOfWork.TransactionRepository.Insert(transactionFrom);
                //update balance of AccountFrom
                await _mediator.Send(new UpdateBalanceAccountRequest()
                {
                    AccountId = accountFrom.Id,
                    Value = transactionFrom.Value,
                    FlowType = transactionFrom.FlowType
                });

                TransactionBank transactionTo = new TransactionBank()
                {
                    AccountFromId = accountFrom.Id,
                    AccountToId = accountTo.Id,
                    TransactionType = Domain.Enums.TransactionType.TRANSFERENCIA,
                    Value = request.Value,
                    FlowType = Domain.Enums.FlowType.ENTRADA,
                    AccountId = accountTo.Id
                };


                _unitOfWork.TransactionRepository.Insert(transactionTo);
                //update balance of to
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
                throw new Exception("Erro ao efetuar Transferência", ex);
            }
        }

    }
}
