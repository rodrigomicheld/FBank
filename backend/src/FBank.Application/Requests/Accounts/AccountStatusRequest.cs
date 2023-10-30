using FBank.Application.Interfaces;
using FBank.Domain.Enums;
using MediatR;

namespace FBank.Application.Requests.Accounts
{
    public class AccountStatusRequest : IRequest<string>, IPersistable
    {
        public int AccountNumber { get; set; }
        public AccountStatus AccountStatus { get; set; }
    }
}
