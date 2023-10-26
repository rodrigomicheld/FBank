//using FBank.Application.Interfaces;
//using FBank.Domain.Entities;

//namespace FBank.Infrastructure.Repositories
//{
//    public class UnitOfWork : IUnitOfWork, IDisposable
//    {
//        private DataBaseContext _contexto = null;
//        private IBaseRepository<Client> clientRepository = null;
//        private IBaseRepository<Bank> bankRepository = null;
//        private IBaseRepository<Agency> agencyRepository = null;
//        private IBaseRepository<Transaction> transactionRepository=null;
        
//        public UnitOfWork()
//        {
//            _contexto = new DataBaseContext();
//        }
//        public void Commit()
//        {
//            _contexto.SaveChanges();
//        }

//        public IBaseRepository<Client> Clients
//        {
//            get
//            {
//                if (clientRepository == null)
//                {
//                    clientRepository = new BaseRepository<Client>(_contexto);
//                }
//                return clientRepository;
//            }
//        }

//        public IBaseRepository<Bank> Banks
//        {
//            get
//            {
//                if (bankRepository == null)
//                {
//                    bankRepository = new BaseRepository<Bank>(_contexto);
//                }
//                return bankRepository;
//            }
//        }

//        public IBaseRepository<Agency> Agencies
//        {
//            get
//            {
//                if (agencyRepository == null)
//                {
//                    agencyRepository = new BaseRepository<Agency>(_contexto);
//                }
//                return agencyRepository;
//            }
//        }

//        public IBaseRepository<Transaction> Transactions
//        {
//            get
//            {
//                if (transactionRepository == null)
//                {
//                    transactionRepository = new BaseRepository<Transaction>(_contexto);
//                }
//                return transactionRepository;
//            }
//        }

//        private bool disposed = false;
//        protected virtual void Dispose(bool disposing)
//        {
//            if (!this.disposed)
//            {
//                if (disposing)
//                {
//                    _contexto.Dispose();
//                }
//            }
//            this.disposed = true;
//        }
//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }
//    }
//}
