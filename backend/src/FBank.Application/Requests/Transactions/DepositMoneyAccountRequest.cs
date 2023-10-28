using FBank.Application.ViewMoldels;
using MediatR;

namespace FBank.Application.Requests.Transactions
{
    public class DepositMoneyAccountRequest : IRequest<TransactionViewModel>
    {
        public int AgencyCode { get; set; }
        public int AccountNumber { get; set; }
        public decimal Value { get; set; }

    }
}
