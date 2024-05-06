namespace Application.Interfaces
{
    public interface IUnitOfWork
    {
        public IClientRepository ClientRepository { get; }
        public IBankRepository BankRepository { get; }
        public IAgencyRepository AgencyRepository { get; }
        public ITransactionRepository TransactionRepository { get; }
        public IAccountRepository AccountRepository { get; }

        void Commit();
        void Rollback();
    }
}
