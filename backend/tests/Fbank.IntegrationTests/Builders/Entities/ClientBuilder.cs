using FBank.Domain.Entities;
using FBank.Domain.Enums;

namespace Fbank.IntegrationTests.Builders.Entities
{
    public class ClientBuilder
    {
        private string? _name;
        public string? _document;
        public PersonType? _personType;
        public IList<Account> _accounts = new List<Account>();


        public Client Build()
        {
            return new Client
            {
                Name = _name ?? "Cliente Test",
                Document = _document ?? "12345678",
                DocumentType = _personType ?? PersonType.Person,
                Password = "123",
                Accounts = _accounts ?? new List<Account>()
                {
                  
                }
            };
        }

        public ClientBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public ClientBuilder WithDocument(string document)
        {
            _document = document;
            return this;
        }

        public ClientBuilder WithPersonType(PersonType personType)
        {
            _personType = personType;
            return this;
        }

        public ClientBuilder WithAccounts(IEnumerable<Account> accounts)
        {
            _accounts = (IList<Account>)accounts;
            return this;
        }
    }
}
