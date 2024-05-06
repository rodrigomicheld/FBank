using System.ComponentModel;

namespace Domain.Enums
{
    public enum TransactionType
    {
        [Description("Pagamento")]
        PAYMENT = 0,
        [Description("Transferência")]
        TRANSFER = 1,
        [Description("Depósito")]
        DEPOSIT = 2,
        [Description("Saque")]
        WITHDRAW = 3
    }

}
