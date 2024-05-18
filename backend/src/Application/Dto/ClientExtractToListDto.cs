using Domain.Enums;
using System.Diagnostics.CodeAnalysis;

namespace Application.Dto
{
    
    public class ClientExtractToListDto
    {
        public Guid IdTransaction { get; set; }
        public DateTime DateTransaction { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public Guid IdAccountOrigin { get; set; }
        public Guid IdAccountDestination { get; set; }
    }
}
