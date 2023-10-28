using Fbank.IntegrationTests.Builders.Entities;
using FBank.Application.Queries;
using FBank.Application.Requests;
using FBank.Application.ViewMoldels;
using FBank.Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Fbank.IntegrationTests.Application.Queries
{
    public class GetOneClientQueryHandleTests : ApplicationTestBase
    {
        public GetOneClientQueryHandleTests(WebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Shoud_return_exception_when_client_not_found()
        {
            const string DOCUMENT = "34425915038";

            var query = new GetOneClientQuery { Document = DOCUMENT };

            Func<Task> handle = async () => await Handle<GetOneClientQuery, ClientViewModel>(query);

            await handle.Should().ThrowAsync<NullReferenceException>().WithMessage("Client not found!");
        }

        [Fact]
        public async Task Shoud_list_client_requested_correctly()
        {
            const string NAME = "cliente Teste";
            const string DOCUMENT = "34425915038";
            const string PASSWORD = "password";
            Guid bankId = Guid.NewGuid();

            InsertOne(new BankBuilder().WithCode(1).WithId(bankId).Build());
            InsertOne(new AgencyBuilder().WithCode(1).WithBankId(bankId).Build());
            
            await Handle<PostOneClientRequest, string>(new PostOneClientRequest { Document = DOCUMENT, Name = NAME, Password = PASSWORD });

            var response = await Handle<GetOneClientQuery, ClientViewModel>(new GetOneClientQuery { Document = DOCUMENT });

            var client = GetEntities<Client>();
            
            Assert.Equal(client.First().Id, response.Id);
            Assert.Equal(DOCUMENT, response.Document);
            Assert.Equal(NAME, response.Name);
            Assert.Equal("Fisica", response.PersonType);
            Assert.NotNull(response.Accounts);
        }
    }
}
