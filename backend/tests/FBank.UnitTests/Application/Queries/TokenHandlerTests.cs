using FBank.Application.Interfaces;
using FBank.Application.Requests;
using FBank.Application.Services;
using FBank.Domain.Entities;
using FBank.Domain.Enums;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Linq.Expressions;

namespace FBank.UnitTests.Application.Queries
{
    public class TokenHandlerTests
    {
        
        private readonly IClientRepository _mockClientRepository;
        private readonly ILogger<TokenRequestHandler> _mockLogger;
        private readonly ITokenService _mockTokenService;


        public TokenHandlerTests()
        {
            _mockClientRepository = Substitute.For<IClientRepository>();

            _mockClientRepository = Substitute.For<IClientRepository>();
            _mockLogger = Substitute.For<ILogger<TokenRequestHandler>>();
            _mockTokenService = Substitute.For<ITokenService>();
        }

        [Fact]
        public void Should_return_ArgumentException_when_client_invalid()
        {
            var query = new TokenRequest { NumberAgency = 1, NumberAccount = 2, Password = "test" };

            _mockClientRepository.SelectOne(Arg.Any<Expression<Func<Client, bool>>>()).ReturnsNull();

            var handler = new TokenRequestHandler(_mockClientRepository, _mockLogger, _mockTokenService);           

            Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public void Should_return_token_when_client_and_password_correct()
        {
            var query = new TokenRequest { NumberAgency = 1, NumberAccount = 2, Password = "123" };

            var client = new Client
            {
                Id = Guid.NewGuid(),
                Name = "test",
                Document = "29209320042",
                DocumentType = PersonType.Person,
                Password = "123",
            };

            var agency = new Agency
            {
                Id = Guid.NewGuid(),
                Name = "Agency test",
                Code = 1
            };

            client.Accounts = new List<Account>() 
            { 
                new Account 
                { 
                    ClientId = client.Id,
                    AgencyId = agency.Id,
                    Number = 2,
                    Status = AccountStatusEnum.Active
                } 
            };

            _mockClientRepository.SelectOne(Arg.Any<Expression<Func<Client, bool>>>()).Returns(client);

            var handler = new TokenRequestHandler(_mockClientRepository, _mockLogger, _mockTokenService);

            var response = handler.Handle(query, CancellationToken.None);

            Assert.NotNull(response);
        }
    }
}
