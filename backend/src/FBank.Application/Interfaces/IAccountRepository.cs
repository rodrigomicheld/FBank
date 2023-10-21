using FBank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FBank.Application.Interfaces
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
    }
}
