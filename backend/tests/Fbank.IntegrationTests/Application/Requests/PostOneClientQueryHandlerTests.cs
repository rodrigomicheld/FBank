using FBank.Application.Requests;
using FBank.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;

namespace Fbank.IntegrationTests.Application.Requests
{
    public class PostOneClientQueryHandlerTests : ApplicationTestBase
    {
        public PostOneClientQueryHandlerTests(WebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task deve_inserir_clientAsync()
        {
            const string NAME = "cliente Teste";
            const string DOCUMENT = "02197627597";

            var request = new PostOneClientRequest { Document = DOCUMENT, Name = NAME };

            await Handle<PostOneClientRequest, Guid>(request);

            var client = GetEntities<Client>();
            
            client.Should().HaveCount(1);
            client.First().Name = NAME;
            client.Last().Document = DOCUMENT;

        }
    }
}
