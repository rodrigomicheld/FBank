using FBank.Application.Interfaces;
using FBank.Application.Queries;
using FBank.Application.Services;
using FBank.Domain.Entities;
using FBank.Domain.Enums;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Linq.Expressions;

namespace FBank.UnitTests.Application.Queries
{
    public class PostOneClientQueryHandlerTests
    {
        
        private readonly IClientRepository _mockClientRepository;
        private readonly IAgencyRepository _mockAgencyRepository;
        private readonly IAccountRepository _mockAccountRepository;
        private readonly ILogger<PostOneClientQueryHandler> _mockLogger;


        public PostOneClientQueryHandlerTests()
        {
            _mockClientRepository = Substitute.For<IClientRepository>();

            _mockClientRepository = Substitute.For<IClientRepository>();
            _mockAgencyRepository = Substitute.For<IAgencyRepository>();
            _mockAccountRepository = Substitute.For<IAccountRepository>();
            _mockLogger = Substitute.For<ILogger<PostOneClientQueryHandler>>();
        }

        [Fact]
        public void Should_return_ArgumentException_when_document_invalid()
        {
            var query = new PostOneClientQuery { Document = "1234", Name = "test" };

            var handler = new PostOneClientQueryHandler(_mockClientRepository, _mockLogger, _mockAgencyRepository, _mockAccountRepository);           

            Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public void Should_return_InvalidOperationException_when_client_have_an_active_account()
        {
            var query = new PostOneClientQuery { Document = "29209320042", Name = "test" };

            var client = new Client
            {
                Id = Guid.NewGuid(),
                Name = "test",
                Document = "29209320042",
                DocumentType = PersonType.Person,
            };

            var agency = new Agency
            {
                Id = Guid.NewGuid(),
                Name = "Agency test"
            };

            client.Accounts = new List<Account>() 
            { 
                new Account 
                { 
                    ClientId = client.Id,
                    AgencyId = agency.Id,
                    Number = 1,
                    Status = AccountStatusEnum.Active
                } 
            };

            _mockClientRepository.SelectOne(Arg.Any<Expression<Func<Client, bool>>>()).Returns(client);
            _mockAgencyRepository.SelectOne(Arg.Any<Expression<Func<Agency, bool>>>()).Returns(agency);

            var handler = new PostOneClientQueryHandler(_mockClientRepository, _mockLogger, _mockAgencyRepository, _mockAccountRepository);

            Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public void Should_insert_client_and_return_account()
        {
            var query = new PostOneClientQuery { Document = "29209320042", Name = "test" };

            var agency = new Agency
            {
                Id = Guid.NewGuid(),
                Name = "Agency test"
            };

            _mockClientRepository.SelectOne(Arg.Any<Expression<Func<Client, bool>>>()).ReturnsNull();
            _mockAgencyRepository.SelectOne(Arg.Any<Expression<Func<Agency, bool>>>()).Returns(agency);

            var handler = new PostOneClientQueryHandler(_mockClientRepository, _mockLogger, _mockAgencyRepository, _mockAccountRepository);

            var response = handler.Handle(query, CancellationToken.None);

            Assert.NotNull(response);
        }

        [Fact]
        public void Should_insert_account_when_inactive()
        {
            var query = new PostOneClientQuery { Document = "29209320042", Name = "test" };

            var client = new Client
            {
                Id = Guid.NewGuid(),
                Name = "test",
                Document = "29209320042",
                DocumentType = PersonType.Person,
            };

            var agency = new Agency
            {
                Id = Guid.NewGuid(),
                Name = "Agency test"
            };

            client.Accounts = new List<Account>()
            {
                new Account
                {
                    ClientId = client.Id,
                    AgencyId = agency.Id,
                    Number = 1,
                    Status = AccountStatusEnum.Inactive
                }
            };

            _mockClientRepository.SelectOne(Arg.Any<Expression<Func<Client, bool>>>()).Returns(client);
            _mockAgencyRepository.SelectOne(Arg.Any<Expression<Func<Agency, bool>>>()).Returns(agency);

            var handler = new PostOneClientQueryHandler(_mockClientRepository, _mockLogger, _mockAgencyRepository, _mockAccountRepository);

            var response = handler.Handle(query, CancellationToken.None);

            Assert.NotNull(response);
        }
    }
}
