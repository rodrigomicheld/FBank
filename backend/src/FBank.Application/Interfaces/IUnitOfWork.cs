using FBank.Domain.Entities;

namespace FBank.Application.Interfaces
{
    public interface IUnitOfWork
    {
        public IBaseRepository<Client> ClientRepository { get; }
        public IBaseRepository<Bank> BankRepository { get; }
        public IBaseRepository<Agency> AgencyRepository { get; }
        public IBaseRepository<Transaction> TransactionRepository { get; }
        public IBaseRepository<Account> AccountRepository { get; }

        void Commit();
        void Rollback();
    }
}
