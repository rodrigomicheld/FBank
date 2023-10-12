using FBank.Application.ViewMoldels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBank.Application.Requests
{
    public class DepositMoneyAccountRequest : IRequest<TransactionViewModel>
    {
    }
}
