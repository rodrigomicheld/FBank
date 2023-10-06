using FBank.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FBank.Domain.Entities
{
    public  class Account : EntityBase
    {
        public Guid ClientId { get; set; }
        public virtual Client Client { get; set; }
        public Guid AgencyId { get; set; }
        public virtual Agency Agency { get; set; }

        [EnumDataType(typeof(AccountStatusEnum))]
        public int IdStatus { get; set; }
        public Decimal Balance { get; set; }
        public virtual IEnumerable<TransactionBank> Transactions { get; set; }
    }
}
