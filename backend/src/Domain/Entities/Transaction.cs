using Domain.Enums;
using System;

namespace Domain.Entities
{
    public class Transaction : EntityBase
    {
        public TransactionType TransactionType { get; set; }
        public decimal Value { get; set; }
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }
        public Guid? AccountToId { get; set; }
        public FlowType FlowType { get; set; }  
    }
}

