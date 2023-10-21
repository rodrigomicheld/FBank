using FBank.Application.Interfaces;
using FBank.Application.Requests;
using FBank.Application.Services;
using FBank.Domain.Entities;
using FBank.Domain.Enums;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Linq.Expressions;

namespace FBank.UnitTests.Application.Requests
{
    public class PostOneClientRequestHandlerTests
    {

        private readonly IClientRepository _mockClientRepository;
        private readonly IAgencyRepository _mockAgencyRepository;
        private readonly IAccountRepository _mockAccountRepository;
        private readonly ILogger<PostOneClientRequestHandler> _mockLogger;


        public PostOneClientRequestHandlerTests()
        {
            _mockClientRepository = Substitute.For<IClientRepository>();

            _mockClientRepository = Substitute.For<IClientRepository>();
            _mockAgencyRepository = Substitute.For<IAgencyRepository>();
            _mockAccountRepository = Substitute.For<IAccountRepository>();
            _mockLogger = Substitute.For<ILogger<PostOneClientRequestHandler>>();
        }

        [Fact]
        public void Should_return_ArgumentException_when_document_invalid()
        {
            var query = new PostOneClientRequest { Document = "1234", Name = "test" };

            var handler = new PostOneClientRequestHandler(_mockClientRepository, _mockAgencyRepository, _mockAccountRepository, _mockLogger);

            Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public void Should_insert_client_and_return_account()
        {
            var query = new PostOneClientRequest { Document = "29209320042", Name = "test" };

            var agency = new Agency
            {
                Id = Guid.NewGuid(),
                Name = "Agency test"
            };

            _mockClientRepository.SelectOne(Arg.Any<Expression<Func<Client, bool>>>()).ReturnsNull();
            _mockAgencyRepository.SelectOne(Arg.Any<Expression<Func<Agency, bool>>>()).Returns(agency);

            var handler = new PostOneClientRequestHandler(_mockClientRepository, _mockAgencyRepository, _mockAccountRepository, _mockLogger);

            var response = handler.Handle(query, CancellationToken.None);

            Assert.NotNull(response);
        }       
    }
}
