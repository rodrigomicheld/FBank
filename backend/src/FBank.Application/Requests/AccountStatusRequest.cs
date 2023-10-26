using FBank.Domain.Enums;
using MediatR;

namespace FBank.Application.Requests
{
    public class AccountStatusRequest :  IRequest<string>
    {        
        public int AccountNumber { get; set; }                
        public AccountStatus AccountStatus { get; set; }
    }
}
