using Domain.Entities;
using Domain.Enums;

namespace Application.ViewMoldels
{
    public class TransactionViewModel
    {
        public decimal Amount { get; set; }
        public DateTime DateTransaction { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
