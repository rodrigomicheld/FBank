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
        private readonly ILogger<PostOneClientRequestHandler> _mockLogger;
        private readonly IUnitOfWork _unitOfWork;

        public PostOneClientRequestHandlerTests()
        {
            _mockLogger = Substitute.For<ILogger<PostOneClientRequestHandler>>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
        }

        [Fact]
        public void Should_return_ArgumentException_when_document_invalid()
        {
            var query = new PostOneClientRequest { Document = "1234", Name = "test" };
            var handler = new PostOneClientRequestHandler(_mockLogger, _unitOfWork);

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

            _unitOfWork.ClientRepository.SelectOne(Arg.Any<Expression<Func<Client, bool>>>()).ReturnsNull();
            _unitOfWork.AgencyRepository.SelectOne(Arg.Any<Expression<Func<Agency, bool>>>()).Returns(agency);

            var handler = new PostOneClientRequestHandler(_mockLogger, _unitOfWork);

            var response = handler.Handle(query, CancellationToken.None);

            Assert.NotNull(response);
        }
    }
}
