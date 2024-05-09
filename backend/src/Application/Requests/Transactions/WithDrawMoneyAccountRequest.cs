using Application.Interfaces;
using Application.ViewMoldels;
using MediatR;

namespace Application.Requests.Transactions
{
    public class WithDrawMoneyAccountRequest : IRequest<TransactionViewModel>, IPersistable
    {
        public int AccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
