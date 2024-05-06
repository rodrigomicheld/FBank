using Domain.Enums;

namespace Domain.Entities
{
    public  class Account : EntityBase
    {
        public Guid ClientId { get; set; }
        public virtual Client Client { get; set; }
        public Guid AgencyId { get; set; }
        public virtual Agency Agency { get; set; }
        public AccountStatus Status { get; set; }
        public decimal Balance { get; set; }
        public virtual IEnumerable<Transaction> Transactions { get; set; }
        public int Number { get; set; }
    }
}
