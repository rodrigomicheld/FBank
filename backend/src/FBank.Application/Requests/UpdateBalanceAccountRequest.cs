using FBank.Application.ViewMoldels;
using FBank.Domain.Entities;
using FBank.Domain.Enums;
using MediatR;

namespace FBank.Application.Requests
{
    public class UpdateBalanceAccountRequest : IRequest<UpdateBalanceViewModel>
    {
        public Guid AccountId { get; set; }
        public decimal Value { get; set; }
        public FlowType FlowType { get; set; }
    }
}
