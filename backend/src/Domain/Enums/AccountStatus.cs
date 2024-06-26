﻿using System.ComponentModel;

namespace Domain.Enums
{
    public enum AccountStatus
    {

        [Description("Ativo")]
        Active = 1,
        [Description("Inativo")]
        Inactive = 2,
        [Description("Em Processamento")]
        InPrgress = 3,
        [Description("Cancelado")]
        Cancelado = 4
    }
}
