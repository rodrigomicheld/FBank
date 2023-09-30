using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBank.Domain.Entities
{
    public  class Account : EntityBase
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int IdStatus { get; set; }
        public Decimal Saldo { get; set; }
    }
}
