using FBank.Application.Interfaces;

namespace FBank.Application.Service
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository=transactionRepository;
        }

        public string Sacar(decimal amount)
        {
            return $"Valor sacado foi de {amount}";
        }
    }
}
