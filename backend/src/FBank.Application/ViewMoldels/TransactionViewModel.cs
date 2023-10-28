using FBank.Domain.Entities;
using FBank.Domain.Enums;

namespace FBank.Application.ViewMoldels
{
    public class TransactionViewModel
    {
        public decimal Amount { get; set; }
        public DateTime DateTransaction { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
