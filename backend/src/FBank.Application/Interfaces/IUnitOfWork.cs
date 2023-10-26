using FBank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBank.Application.Interfaces
{
    public interface IUnitOfWork
    {
        public IBaseRepository<Client> Clients { get; }
        public IBaseRepository<Bank> Banks { get; }
        public IBaseRepository<Agency> Agencies { get; }
        public IBaseRepository<Transaction> Transactions { get; }

        void Commit();
    }
}
