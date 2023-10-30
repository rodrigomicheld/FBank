using FBank.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FBank.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ClientRepository _clientRepository;
        private BankRepository _bankRepository;
        private AgencyRepository _agencyRepository;
        private TransactionRepository _transactionRepository;
        private AccountRepository _accountRepository;

        private DataBaseContext _context;
        public UnitOfWork(DataBaseContext context)
        {
            _context = context;
        }
        public IClientRepository ClientRepository
        {
            get
            {
                if (_clientRepository == null)
                {
                    _clientRepository = new ClientRepository(_context);
                }
                return _clientRepository;
            }
        }

        public IBankRepository BankRepository
        {
            get
            {
                if (_bankRepository == null)
                {
                    _bankRepository = new BankRepository(_context);
                }
                return _bankRepository;
            }
        }
        public IAgencyRepository AgencyRepository
        {
            get
            {
                if (_agencyRepository == null)
                {
                    _agencyRepository = new AgencyRepository(_context);
                }
                return _agencyRepository;
            }
        }
        public ITransactionRepository TransactionRepository
        {
            get
            {
                if (_transactionRepository == null)
                {
                    _transactionRepository = new TransactionRepository(_context);
                }
                return _transactionRepository;
            }
        }
        public IAccountRepository AccountRepository
        {
            get
            {
                if (_accountRepository == null)
                {
                    _accountRepository = new AccountRepository(_context);
                }
                return _accountRepository;
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
        public void Rollback()
        {
            foreach (var entry in _context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified://??
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
            _context.Dispose();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
