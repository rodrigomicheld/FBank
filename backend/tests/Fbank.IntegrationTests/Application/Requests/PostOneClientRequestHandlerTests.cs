using FBank.Application.Requests;
using FBank.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using FBank.Domain.Enums;
using Fbank.IntegrationTests.Builders.Entities;

namespace Fbank.IntegrationTests.Application.Requests
{
    public class PostOneClientRequestHandlerTests : ApplicationTestBase
    {
        public PostOneClientRequestHandlerTests(WebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_insert_client_correctly()
        {
            const string NAME = "cliente Teste";
            const string DOCUMENT = "34425915038";
            const string PASSWORD = "password";
            Guid bankId = Guid.NewGuid();

            InsertOne(new BankBuilder().WithCode(1).WithId(bankId).Build());
            InsertOne(new AgencyBuilder().WithCode(1).WithBankId(bankId).Build());

            var request = new PostOneClientRequest { Document = DOCUMENT, Name = NAME, Password = PASSWORD };

            await Handle<PostOneClientRequest, string>(request);

            var client = GetEntities<Client>();           
            client.Should().HaveCount(1);

            client.First().Name = NAME;
            client.First().Document = DOCUMENT;
            client.First().Password.Should().NotBeNull();
            client.First().DocumentType = PersonType.Person;

            var account = GetEntities<Account>();
            account.Should().HaveCount(1);

            account.First().Status = AccountStatusEnum.Active;
            account.First().Number = 1;
            account.First().Balance = 0M;
            account.First().ClientId = client.First().Id;
        }
    }
}