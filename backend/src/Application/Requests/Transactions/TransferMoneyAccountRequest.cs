using Application.Interfaces;
using Application.ViewMoldels;
using MediatR;

namespace Application.Requests.Transactions
{
    public class TransferMoneyAccountRequest : IRequest<string>, IPersistable
    {
        public int AccountNumberFrom { get; set; }
        public int AccountNumberTo { get; set; }
        public decimal Value { get; set; }
    }
}
