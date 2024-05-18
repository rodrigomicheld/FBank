using AutoMapper;
using Application.Interfaces;
using Application.Requests.Transactions;
using Application.Services.Transactions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System.Linq.Expressions;
using Application.Mappers;
using Microsoft.Extensions.DependencyInjection;

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

        [Fact]
        public async Task Should_return_EmptyObject_When_Operation_Value_Is_0()
        {
            var request = new DepositMoneyAccountRequest
            {
                AccountNumber = 1,
                AgencyCode = 1,
                Value = 0
            };

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.Equal(0, result.Amount);
            Assert.Equal(DateTime.MinValue, result.DateTransaction);
        }

        [Fact]
        public async Task Should_Return_Exception_When_Account_Is_Not_Nound()
        {
            var request = new DepositMoneyAccountRequest
            {
                AccountNumber = 1,
                AgencyCode = 1,
                Value = 10
            };

            _mockUnitOfWork.AccountRepository.SelectOne(Arg.Any<Expression<Func<Account, bool>>>()).Returns((Account)null);


            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(request, CancellationToken.None));
            Assert.Contains("Account not found", ex.Message);
        }

        [Fact]
        public async Task Should_Return_Exception_When_Account_Is_Inactive()
        {
            var request = new DepositMoneyAccountRequest
            {
                AccountNumber = 1,
                AgencyCode = 1,
                Value = 10
            };

            var fakeAccount = FakeData.Account(Domain.Enums.AccountStatus.Inactive);

            _mockUnitOfWork.AccountRepository.SelectOne(Arg.Any<Expression<Func<Account, bool>>>()).Returns(fakeAccount);


            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(request, CancellationToken.None));
            Assert.Contains("Account is Inactive!", ex.Message);
        }
    }
}
