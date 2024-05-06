using Application.Interfaces;
using Domain.Enums;
using MediatR;

namespace Application.Requests.Transactions
{
    public class UpdateBalanceAccountRequest : IRequest<Unit>
    {
        public Guid AccountId { get; set; }
        public decimal Value { get; set; }
        public FlowType FlowType { get; set; }
    }
}
