using FBank.Domain.Entities;

namespace Fbank.IntegrationTests.Builders.Entities
{
    public class AgencyBuilder
    {
        private string? _name;
        private int? _code;
        private Guid? _bankId;
        private Guid? _agencyId;
       
        public Agency Build()
        {
            return new Agency
            {
                Id = _agencyId ?? Guid.NewGuid(),
                Code = _code ?? 1,
                Name = _name ?? "Agencia Test",
                BankId = _bankId ?? Guid.NewGuid(),
                CreateDateAt = DateTime.Now,
                UpdateDateAt = DateTime.Now
            };
        }

        public AgencyBuilder WithId(Guid id)
        {
            _agencyId = id;
            return this;
        }

        public AgencyBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public AgencyBuilder WithCode(int code)
        {
            _code = code;
            return this;
        }

        public AgencyBuilder WithBankId(Guid bankId)
        {
            _bankId = bankId;
            return this;
        }
    }
}
