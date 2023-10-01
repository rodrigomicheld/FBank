using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBank.Domain.Enums
{
    public enum AccountStatusEnum
    {

        [Description("Active")]
        Active = 1,
        [Description("Inactive")]
        Inactive = 2,
        [Description("In Progress")]
        InPrgress = 3,
        [Description("Canceled")]
        Cancelado = 4
    }
}
