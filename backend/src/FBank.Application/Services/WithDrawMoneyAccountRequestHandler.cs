using FBank.Application.Interfaces;
using FBank.Application.Requests;
using FBank.Application.ViewMoldels;
using MediatR;

namespace FBank.Application.Services
{
    public class WithDrawMoneyAccountRequestHandler : IRequestHandler<WithDrawMoneyAccountRequest, TransactionViewModel>
    {
        private readonly ITransactionRepository _transactionRepository;

        public WithDrawMoneyAccountRequestHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public Task<TransactionViewModel> Handle(WithDrawMoneyAccountRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public decimal WithDraw(decimal amount)
        {
            return amount;
        }
    }
}
