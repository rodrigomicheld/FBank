using AutoMapper;
using FBank.Application.Interfaces;
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

        public DepositMoneyAccountHandlerTests()
        {
            _mockTransactionRepository = Substitute.For<ITransactionRepository>();
            _mockAccountRepository = Substitute.For<IAccountRepository>();
            _mockMediator = Substitute.For<IMediator>();

            _mockTransactionRepository.SelectToId(Arg.Any<Guid>()).Returns(new TransactionBank());
            _query = new DepositMoneyAccountRequest();
            _handler = new DepositMoneyAccountHandler(
                _mockMediator,
                _mockTransactionRepository,
                _mockAccountRepository,
                Substitute.For<ILogger<DepositMoneyAccountHandler>>(),
                Substitute.For<IMapper>());
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
