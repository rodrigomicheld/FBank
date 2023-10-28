using FBank.Application.ViewMoldels;
using MediatR;

namespace FBank.Application.Requests.Transactions
{
    public class WithDrawMoneyAccountRequest : IRequest<TransactionViewModel>
    {
        public int AccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
