using FBank.Domain.Entities;

namespace Fbank.IntegrationTests.Builders.Entities
{
    public class AgencyBuilder
    {
        private string? _name;
        public int? _code;
        public Guid? _bankId;
       
        public Agency Build()
        {
            return new Agency
            {
                Id = Guid.NewGuid(),
                Code = _code ?? 1,
                Name = _name ?? "Agencia Test",
                BankId = _bankId ?? Guid.NewGuid(),
                CreateDateAt = DateTime.Now,
                UpdateDateAt = DateTime.Now
            };
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
