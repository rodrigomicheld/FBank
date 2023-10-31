using FBank.Application.Interfaces;
using FBank.Domain.Enums;
using MediatR;

namespace FBank.Application.Requests.Transactions
{
    public class UpdateBalanceAccountRequest : IRequest<Unit>
    {
        public Guid AccountId { get; set; }
        public decimal Value { get; set; }
        public FlowType FlowType { get; set; }
    }
}
