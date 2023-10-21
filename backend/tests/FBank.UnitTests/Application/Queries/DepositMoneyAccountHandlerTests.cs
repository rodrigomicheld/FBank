using AutoMapper;
using FBank.Application.Interfaces;
using FBank.Application.Queries;
using FBank.Application.Requests;
using FBank.Application.Services;
using FBank.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace FBank.UnitTests.Application.Queries
{
    public class DepositMoneyAccountHandlerTests
    {
        private readonly ITransactionRepository _mockTransactionRepository;
        private readonly IAccountRepository _mockAccountRepository;
        private readonly DepositMoneyAccountRequest _query;
        private readonly DepositMoneyAccountHandler _handler;
        private readonly IMediator _mockMediator;
        private readonly IUnitOfWork _mockUnitOfWork;
        private readonly ILogger<DepositMoneyAccountHandler> _logger;   
        public DepositMoneyAccountHandlerTests()
        {
            _mockUnitOfWork = Substitute.For<IUnitOfWork>();
            _mockTransactionRepository = Substitute.For<ITransactionRepository>();
            _mockAccountRepository = Substitute.For<IAccountRepository>();
            _mockMediator = Substitute.For<IMediator>();
            
            _logger = Substitute.For<ILogger<DepositMoneyAccountHandler>>();    
            _mockTransactionRepository.SelectToId(Arg.Any<Guid>()).Returns(new TransactionBank());

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
            _mockTransactionRepository.SelectToId(Arg.Any<Guid>()).Returns(new TransactionBank());
            var response = _handler.Handle(_query, CancellationToken.None);
            Assert.NotNull(response);
        }

        [Fact]
        public void Should_return_NullReferenceException_when_transaction_not_found()
        {
            _mockTransactionRepository.SelectToId(Arg.Any<Guid>()).Throws(new NullReferenceException());
            Assert.ThrowsAsync<NullReferenceException>(() => _handler.Handle(_query, CancellationToken.None));
        }

    }
}
