using FBank.Application.Interfaces;
using FBank.Application.Requests;
using FBank.Application.ViewMoldels;
using MediatR;

namespace FBank.Application.Services
{
    public class TransferMoneyAccountRequestHandler : IRequestHandler<TransferMoneyAccountRequest, TransferViewModel>
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransferMoneyAccountRequestHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public Task<TransferViewModel> Handle(TransferMoneyAccountRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
