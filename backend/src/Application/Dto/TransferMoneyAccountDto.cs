using System.Diagnostics.CodeAnalysis;

namespace Application.Dto
{
    [ExcludeFromCodeCoverage]
    public class TransferMoneyAccountDto
    {
        public int AccountNumberTo { get; set; }
        public decimal Value { get; set; }
    }
}
