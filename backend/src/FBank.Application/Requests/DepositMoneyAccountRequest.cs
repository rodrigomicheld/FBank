using FBank.Application.ViewMoldels;
using FBank.Domain.Entities;
using FBank.Domain.Enums;
using MediatR;

namespace FBank.Application.Requests
{
    public class DepositMoneyAccountRequest :  IRequest<TransactionViewModel>
    {        
        public int AccountNumber { get; set; }                
        public decimal Value { get; set; }

    }
}
