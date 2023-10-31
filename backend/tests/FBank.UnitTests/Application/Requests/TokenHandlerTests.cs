using FBank.Application.Interfaces;
using FBank.Application.Requests.Login;
using FBank.Application.Services.Login;
using FBank.Domain.Entities;
using FBank.Domain.Enums;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Linq.Expressions;

namespace FBank.UnitTests.Application.Requests
{
    public class TokenHandlerTests
    {

        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly ILogger<TokenRequestHandler> _mockLogger;
        private readonly ITokenService _mockTokenService;


        public TokenHandlerTests()
        {

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockLogger = Substitute.For<ILogger<TokenRequestHandler>>();
            _mockTokenService = Substitute.For<ITokenService>();
        }

        [Fact]
        public void Should_return_ArgumentException_when_client_invalid()
        {
            var query = new TokenRequest { NumberAgency = 1, NumberAccount = 2, Password = "test" };
            _mockUnitOfWork.Setup(s => s.ClientRepository.SelectOne(It.IsAny<Expression<Func<Client, bool>>>()))
                .ReturnsNull();

            var handler = new TokenRequestHandler(_mockUnitOfWork.Object, _mockLogger, _mockTokenService);

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
                    Status = AccountStatus.Active
                } 
            };

            _mockUnitOfWork.Setup(s => s.ClientRepository.SelectOne(It.IsAny<Expression<Func<Client, bool>>>()))
                .Returns(client);

            var handler = new TokenRequestHandler(_mockUnitOfWork.Object, _mockLogger, _mockTokenService);

            var response = handler.Handle(query, CancellationToken.None);

            Assert.NotNull(response);
        }
    }
}
