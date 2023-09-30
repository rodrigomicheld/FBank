using FBank.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FBank.Domain.Entities
{
    public  class Account : EntityBase
    {
        public Guid ClientId { get; set; }
        public Client Client { get; set; }
        public Guid AgencyId { get; set; }
        public Agency Agency { get; set; }

        [EnumDataType(typeof(AccountStatusEnum))]
        public int IdStatus { get; set; }
        public Decimal Saldo { get; set; }
    }
}
