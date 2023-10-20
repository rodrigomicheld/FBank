using FBank.Domain.Entities;

namespace Fbank.IntegrationTests.Builders.Entities
{
    public class BankBuilder
    {
        private string? _name;
        public int? _code;
        public Guid? _id;
       
        public Bank Build()
        {
            return new Bank
            {
                Id = _id ?? Guid.NewGuid(),
                Code = _code ?? 1,
                Name = _name ?? "Bank Test",
                CreateDateAt = DateTime.Now,
                UpdateDateAt = DateTime.Now
            };
        }

        public BankBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public BankBuilder WithCode(int code)
        {
            _code = code;
            return this;
        }

        public BankBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }
    }
}
