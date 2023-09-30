using FonteSaber.Infra.CrossCutting.Enuns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
