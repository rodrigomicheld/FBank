using AutoMapper;
using Application.Interfaces;
using Application.Requests.Transactions;
using Application.Services.Transactions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace UnitTests.Application.Requests
{
    public class DepositMoneyAccountHandlerTests
    {
        private readonly DepositMoneyAccountRequest _query;
        private readonly DepositMoneyAccountHandler _handler;
        private readonly IMediator _mockMediator;
        private readonly IUnitOfWork _mockUnitOfWork;
        private readonly ILogger<DepositMoneyAccountHandler> _logger;   
        public DepositMoneyAccountHandlerTests()
        {
            _mockUnitOfWork = Substitute.For<IUnitOfWork>();
            _mockMediator = Substitute.For<IMediator>();
            
            _logger = Substitute.For<ILogger<DepositMoneyAccountHandler>>();    
            _query = new DepositMoneyAccountRequest();
            _handler = new DepositMoneyAccountHandler(
                _mockMediator,
                _logger,
                Substitute.For<IMapper>(),
                _mockUnitOfWork
                );
        }

        [Fact]
        public void Should_return_transaction_requested()
        {
            _mockUnitOfWork.TransactionRepository.SelectToId(Arg.Any<Guid>()).Returns(new Transaction());
            var response = _handler.Handle(_query, CancellationToken.None);
            Assert.NotNull(response);
        }

        [Fact]
        public void Should_return_NullReferenceException_when_transaction_not_found()
        {
            _mockUnitOfWork.TransactionRepository.SelectToId(Arg.Any<Guid>()).Throws(new NullReferenceException());
            Assert.ThrowsAsync<NullReferenceException>(() => _handler.Handle(_query, CancellationToken.None));
        }

    }
}
