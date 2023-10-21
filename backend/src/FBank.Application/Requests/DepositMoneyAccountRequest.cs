using FBank.Application.ViewMoldels;
using MediatR;

namespace FBank.Application.Requests
{
    public class DepositMoneyAccountRequest :  IRequest<TransactionViewModel>
    {        
        public int AccountNumber { get; set; }                
        public decimal Value { get; set; }

    }
}
