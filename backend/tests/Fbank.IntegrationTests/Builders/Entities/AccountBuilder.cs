using FBank.Domain.Entities;
using FBank.Domain.Enums;


namespace Fbank.IntegrationTests.Builders.Entities
{
    public class AccountBuilder
    {
        private Guid _id;
        private Guid _clientId;
        private Guid _agencyId;
        private AccountStatus? _status;
        private decimal? _balance;
               
        public Account Build() 
        {
            return new Account
            {
                Id = _id,
                ClientId = _clientId,
                AgencyId = _agencyId,
                Status = _status ?? AccountStatus.Active,
                Balance = _balance ?? 0,
            };
        }

        public AccountBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public AccountBuilder WithClientId(Guid clientId)
        {
            _clientId = clientId;
            return this;
        }

        public AccountBuilder WithAgencyId(Guid agencyId)
        {
            _agencyId = agencyId;
            return this;
        }

        public AccountBuilder WithStatus (AccountStatus status)
        {
            _status = status;
            return this;
        }

    }
}