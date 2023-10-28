using FBank.Domain.Enums;
using MediatR;

namespace FBank.Application.Requests
{
    public class UpdateBalanceAccountRequest : IRequest<Unit>
    {
        public Guid AccountId { get; set; }
        public decimal Value { get; set; }
        public FlowType FlowType { get; set; }
    }
}
