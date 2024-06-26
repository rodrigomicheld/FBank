﻿namespace Domain.Entities
{
    public class Agency  : EntityBase
    {
        public int Code { get; set; }
        public string Name { get; set; }        
        public Guid BankId { get; set; }
        public virtual Bank Bank { get; set; }
        public virtual IEnumerable<Account> Accounts { get; set; }
    }
}
