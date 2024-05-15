using System.Diagnostics.CodeAnalysis;

namespace Application.Dto
{
    [ExcludeFromCodeCoverage]
    public class TransferPixMoneyAccountDto
    {
        public int AccountNumberTo { get; set; }
        public int AccountNumberFrom { get; set; }
        public decimal Value { get; set; }
    }
}
