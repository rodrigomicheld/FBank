using FBank.Application.Interfaces;
using FBank.Application.ViewMoldels;
using MediatR;

namespace FBank.Application.Requests.Transactions
{
    public class TransferMoneyAccountRequest : IRequest<TransferViewModel>, IPersistable
    {
        public int AccountNumberFrom { get; set; }
        public int AccountNumberTo { get; set; }
        public decimal Value { get; set; }
    }
}
