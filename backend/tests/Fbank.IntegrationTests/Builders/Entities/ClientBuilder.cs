using FBank.Domain.Entities;
using FBank.Domain.Enums;

namespace Fbank.IntegrationTests.Builders.Entities
{
    public class ClientBuilder
    {
        private Guid _id;
        private string? _name;
        private string? _document;
        private PersonType? _personType;
        

        public Client Build()
        {
            return new Client
            {
                Id = _id,
                Name = _name ?? "Cliente Test",
                Document = _document ?? "12345678",
                DocumentType = _personType ?? PersonType.Person,
                Password = "12345678"
            };
        }

        public ClientBuilder WithId(Guid id)
        {
            _id = id;
            return this;
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
    }
}
