using FBank.Application.ViewMoldels;
using FBank.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBank.Application.Commands
{
    public class DepositMoneyAccountCommand : TransactionBank ,IRequest<TransactionViewModel>
    {
    }
}
