using Application.Interfaces;
using Application.ViewMoldels;
using MediatR;

namespace Application.Requests.Transactions
{
    public class DepositMoneyAccountRequest : IRequest<TransactionViewModel>, IPersistable
    {
        public int AgencyCode { get; set; }
        public int AccountNumber { get; set; }
        public decimal Value { get; set; }

    }
}
