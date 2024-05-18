using Application.Requests.Login;
using FluentAssertions;
using IntegrationTests.Builders.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace IntegrationTests.Application.Requests
{
    public class TokenHandlerTests : ApplicationTestBase
    {
        [Fact]
        public async Task Should_return_token_when_client_and_password_correct()
        {
            var bank = new BankBuilder().WithCode(1).Build();

            InsertOne(bank);

            var agency = new AgencyBuilder().WithCode(1).WithBankId(bank.Id).Build();
            InsertOne(agency);

            var client = new ClientBuilder().Build();
            InsertOne(client);

            var account = new AccountBuilder().WithAgencyId(agency.Id).WithClientId(client.Id).Build();
            InsertOne(account);

            var request = new TokenRequest { Document = client.Document, Password = client.Password };

            var response = await Handle<TokenRequest, string>(request);

            Assert.NotNull(response);

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(response);

            var documentClaim = token.Claims.FirstOrDefault(c => c.Type == "Document");
            Assert.NotNull(documentClaim);
            Assert.Equal(client.Document, documentClaim.Value);
        }

        [Fact]
        public async Task Should_return_exception_when_client_and_password_incorrect()
        {
            var bank = new BankBuilder().WithCode(1).Build();

            InsertOne(bank);

            var agency = new AgencyBuilder().WithCode(1).WithBankId(bank.Id).Build();
            InsertOne(agency);

            var client = new ClientBuilder().Build();
            InsertOne(client);

            var account = new AccountBuilder().WithAgencyId(agency.Id).WithClientId(client.Id).Build();
            InsertOne(account);

            var request = new TokenRequest { Document = client.Document, Password = "1234" };

            Func<Task> handle = async () => await Handle<TokenRequest, string>(request);

            await handle.Should().ThrowAsync<ArgumentException>().WithMessage("Incorrect account or password!");
        }
    }
}
