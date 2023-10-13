using FBank.Domain.Enums;
using System;

namespace FBank.Domain.Entities
{
    public class TransactionBank : EntityBase
    {
        public TransactionType TransactionType { get; set; }
        public decimal Value { get; set; }
        public Guid AccountToId { get; set; }
        public virtual Account AccountTo { get; set; }
        public Guid  AccountFromId { get; set; }

        public FlowType FlowType { get; set; }  
    }
}

