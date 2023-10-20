using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Identity.Client.Extensibility;

namespace Fbank.IntegrationTests.Application.Queries
{
    public class GetOneClientQueryHandlerTests : ApplicationTestBase
    {
        public GetOneClientQueryHandlerTests(WebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public void deve_retornar_cliente()
        {
           
        }
    }
}
