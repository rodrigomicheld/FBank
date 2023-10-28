using FBank.Application.ViewMoldels;
using MediatR;

namespace FBank.Application.Requests
{
    public class WithDrawMoneyAccountRequest : IRequest<TransactionViewModel>
    {
        public Guid AccountOrigin { get; set; }
        public decimal Amount { get; set; }
    }
}
