using FBank.Application.ViewMoldels;
using MediatR;

namespace FBank.Application.Requests.Transactions
{
    public class DepositMoneyAccountRequest : IRequest<TransactionViewModel>
    {
        public int Agency { get; set; }
        public int Account { get; set; }
        public decimal Value { get; set; }

    }
}
