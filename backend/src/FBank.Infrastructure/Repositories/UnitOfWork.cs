using FBank.Application.Interfaces;
using FBank.Domain.Entities;
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
        private string _errorMessage = string.Empty;
        
        private DataBaseContext _context;
        public UnitOfWork(DataBaseContext context)
        {
            _context = context;
        }
        public IBaseRepository<Client> ClientRepository 
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
        
        public IBaseRepository<Bank> BankRepository
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
        public IBaseRepository<Agency> AgencyRepository
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
        public IBaseRepository<TransactionBank> TransactionRepository
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
        public IBaseRepository<Account> AccountRepository
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
        //public void Save()
        //{
        //    //try
        //    //{
        //        _context.SaveChanges();
        //    //}
        //    //catch (DbEntityValidationException dbEx)
        //    //{
        //    //    foreach (var validationErrors in dbEx.EntityValidationErrors)
        //    //    {
        //    //        foreach (var validationError in validationErrors.ValidationErrors)
        //    //        {
        //    //            _errorMessage = _errorMessage + $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage} {Environment.NewLine}";
        //    //        }
        //    //    }
        //    //    throw new Exception(_errorMessage, dbEx);
        //    //}
        //}


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
