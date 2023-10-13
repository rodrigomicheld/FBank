using FBank.Application.ViewMoldels;
using MediatR;

namespace FBank.Application.Requests
{
    public class TransferMoneyAccountRequest : IRequest<TransferViewModel>
    {
        public int AccountNumberFrom { get; set; }
        public int AccountNumberTo { get; set; }
        public decimal Value { get; set; }
    }
}
