using FBank.Application.ViewMoldels;
using MediatR;

namespace FBank.Application.Requests
{
    public class WithDrawMoneyAccountRequest : IRequest<TransactionViewModel>
    {
        public decimal Amount { get; set; }
    }
}
