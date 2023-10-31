using AutoMapper;
using FBank.Application.Interfaces;
using FBank.Application.Queries.Accounts;
using FBank.Application.Services.Accounts;
using FBank.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System.Linq.Expressions;

namespace FBank.UnitTests.Application.Queries
{
    public class GetOneClientQueryHandlerTests
    {

        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly GetOneClientQuery _query;
        private readonly GetOneClientQueryHandler _handler;
        

        public GetOneClientQueryHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
             _mockUnitOfWork.Setup(s => s.ClientRepository.SelectOne(It.IsAny<Expression<Func<Client, bool>>>())).Returns(new Client());
            _query = new GetOneClientQuery();
            _handler = new GetOneClientQueryHandler(
                _mockUnitOfWork.Object,
                Substitute.For<ILogger<GetOneClientQueryHandler>>(),
                Substitute.For<IMapper>());     
        }

        [Fact]
        public void Should_return_client_requested()
        {
            var response = _handler.Handle(_query, CancellationToken.None);

            Assert.NotNull(response);
        }

        [Fact]
        public void Should_return_NullReferenceException_when_client_not_found()
        {
            _mockUnitOfWork.Setup(s => s.ClientRepository.SelectOne(It.IsAny<Expression<Func<Client, bool>>>())).Throws(new NullReferenceException());
            Assert.ThrowsAsync<NullReferenceException>(() => _handler.Handle(_query, CancellationToken.None));
        }
    }
}
