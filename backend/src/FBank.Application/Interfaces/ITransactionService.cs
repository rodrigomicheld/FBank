using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBank.Application.Interfaces
{
    public interface ITransactionService
    {
        string Sacar(decimal amount);
    }
}
