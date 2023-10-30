using FBank.Application.Interfaces;
using FBank.Application.ViewMoldels;
using MediatR;

namespace FBank.Application.Requests.Transactions
{
    public class WithDrawMoneyAccountRequest : IRequest<TransactionViewModel>, IPersistable
    {
        public int AccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
