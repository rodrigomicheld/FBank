using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FonteSaber.Infra.CrossCutting.Enuns
{
    public enum AccountStatusEnum
    {

        [Description("Ativo")]
        Ativo = 1,
        [Description("Inativo")]
        Inativo = 2,
        [Description("Em Abertura")]
        EmAbertura = 3,
        [Description("Cancelado")]
        Cancelado = 4
    }
}
