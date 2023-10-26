using FBank.Application.Interfaces;
using FBank.Application.Requests;
using FBank.Application.ViewMoldels;
using FBank.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FBank.Application.Services
{
    public class WithDrawMoneyAccountRequestHandler : IRequestHandler<WithDrawMoneyAccountRequest, TransactionViewModel>
    {
        IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly ILogger<WithDrawMoneyAccountRequestHandler> _logger;
        public WithDrawMoneyAccountRequestHandler(IUnitOfWork unitOfWork, IMediator mediator, ILogger<WithDrawMoneyAccountRequestHandler> logger)
        {
            _unitOfWork=unitOfWork;
            _mediator=mediator;
            _logger=logger;
        }

        public async Task<TransactionViewModel> Handle(WithDrawMoneyAccountRequest request, CancellationToken cancellationToken)
        {
            return await WithDraw(request.AccountOrigin, request.Amount);

        }

        public async Task<TransactionViewModel> WithDraw(Guid accountOrigin, decimal amount)
        {
            try
            {
                var account = _unitOfWork.AccountRepository.SelectToId(accountOrigin);

                await _mediator.Send(new UpdateBalanceAccountRequest()
                {
                    AccountId = accountOrigin,
                    Value = amount,
                    FlowType = Domain.Enums.FlowType.SAIDA
                });

                var transfer = new TransactionBank
                {
                    Account = account,
                    AccountFromId = accountOrigin,  
                    AccountToId = accountOrigin,
                    AccountId = accountOrigin,
                    FlowType = Domain.Enums.FlowType.SAIDA,
                    TransactionType = Domain.Enums.TransactionType.SAQUE,
                    Value = amount,
                    CreateDateAt = DateTime.UtcNow,
                    UpdateDateAt = DateTime.UtcNow,
                };

                _unitOfWork.TransactionRepository.Insert(transfer);
                _unitOfWork.Commit();

                return new TransactionViewModel
                {
                    Amount = amount,
                    DateTransaction = transfer.CreateDateAt,
                    TransactionType = Domain.Enums.TransactionType.SAQUE,
                };
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.LogInformation(ex.ToString());
                throw ex;
            }
        }
    }
}
