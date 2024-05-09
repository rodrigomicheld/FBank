using Application.Interfaces;
using Domain.Enums;
using MediatR;

namespace Application.Requests.Accounts
{
    public class AccountStatusRequest : IRequest<string>, IPersistable
    {
        public int AccountNumber { get; set; }
        public AccountStatus AccountStatus { get; set; }
    }
}
