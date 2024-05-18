using Application.Interfaces;
using Application.Requests.Transactions;
using Application.Services.Transactions;
using Application.ViewMoldels;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;

namespace UnitTests.Application.Requests
{
    public class WithDrawMoneyAccountRequestHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ILogger<WithDrawMoneyAccountRequestHandler>> _mockLogger;
        public WithDrawMoneyAccountRequestHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockLogger = new Mock<ILogger<WithDrawMoneyAccountRequestHandler>>();
        }

        [Fact]
        public async void Should_Return_Exception_When_Balance_Is_Less_Than_Withdrawal_Amount()
        {
            var _mockLoggerUpdateBalance = new Mock<ILogger<UpdateBalanceAccountRequestHandler>>();
            var serviceProvider = new ServiceCollection()
                .AddTransient(_ => _mockUnitOfWork.Object)
                .AddTransient(_ => _mockLoggerUpdateBalance.Object)
                .AddTransient<IRequestHandler<UpdateBalanceAccountRequest, Unit>, UpdateBalanceAccountRequestHandler>()
                .AddTransient<IMediator, Mediator>()
                .BuildServiceProvider();

            var mediator = serviceProvider.GetRequiredService<IMediator>();

            var request = new WithDrawMoneyAccountRequest
            {
                AccountNumber = 1,
                Amount = 110
            };

            var mockResponse = new TransactionViewModel
            {
                Amount = 10,
                TransactionType = Domain.Enums.TransactionType.WITHDRAW,
                DateTransaction = DateTime.Now
            };

            _mockUnitOfWork.Setup(s => s.AccountRepository.SelectToId(It.IsAny<Guid>()))
                    .Returns(FakeData.Account());
            _mockUnitOfWork.Setup(s => s.AccountRepository.SelectOne(It.IsAny<Expression<Func<Account, bool>>>())).Returns(FakeData.Account());

            _mockUnitOfWork.Setup(s => s.TransactionRepository.Insert(It.IsAny<Transaction>()));

            var handler = new WithDrawMoneyAccountRequestHandler(_mockUnitOfWork.Object, mediator, _mockLogger.Object);

            var ex = await Assert.ThrowsAsync<Exception>(() => handler.Handle(request, CancellationToken.None));
            Assert.Contains("Insufficient balance", ex.Message);
        }

        [Fact]
        public async void Should_Make_Withdrawal_Balance_Is_Greater()
        {
            // Arrange
            var _mockLoggerUpdateBalance = new Mock<ILogger<UpdateBalanceAccountRequestHandler>>();
            var serviceProvider = new ServiceCollection()
                .AddTransient(_ => _mockUnitOfWork.Object)
                .AddTransient(_ => _mockLoggerUpdateBalance.Object)
                .AddTransient<IRequestHandler<UpdateBalanceAccountRequest, Unit>, UpdateBalanceAccountRequestHandler>()
                .AddTransient<IMediator, Mediator>()
                .BuildServiceProvider();

            var mediator = serviceProvider.GetRequiredService<IMediator>();

            var request = new WithDrawMoneyAccountRequest
            {
                AccountNumber = 1,
                Amount = 10
            };

            var mockResponse = new TransactionViewModel
            {
                Amount = 10,
                TransactionType = Domain.Enums.TransactionType.WITHDRAW,
                DateTransaction = DateTime.Now
            };

            _mockUnitOfWork.Setup(s => s.AccountRepository.SelectOne(It.IsAny<Expression<Func<Account, bool>>>())).Returns(FakeData.Account());
            _mockUnitOfWork.Setup(s => s.AccountRepository.SelectToId(It.IsAny<Guid>())).Returns(FakeData.Account());
            _mockUnitOfWork.Setup(s => s.TransactionRepository.Insert(It.IsAny<Transaction>()));

            var handler = new WithDrawMoneyAccountRequestHandler(_mockUnitOfWork.Object, mediator, _mockLogger.Object);

            var response = await handler.Handle(request, CancellationToken.None);

            Assert.Equal(10, response.Amount);
            Assert.Equal(TransactionType.WITHDRAW, response.TransactionType);
        }

        [Fact]
        public async void Should_Return_Exception_When_Account_Is_Inactive()
        {
            var _mockLoggerUpdateBalance = new Mock<ILogger<UpdateBalanceAccountRequestHandler>>();
            var serviceProvider = new ServiceCollection()
                .AddTransient(_ => _mockUnitOfWork.Object)
                .AddTransient(_ => _mockLoggerUpdateBalance.Object)
                .AddTransient<IRequestHandler<UpdateBalanceAccountRequest, Unit>, UpdateBalanceAccountRequestHandler>()
                .AddTransient<IMediator, Mediator>()
                .BuildServiceProvider();

            var mediator = serviceProvider.GetRequiredService<IMediator>();

            var request = new WithDrawMoneyAccountRequest
            {
                AccountNumber = 1,
                Amount = 110
            };

            var mockResponse = new TransactionViewModel
            {
                Amount = 10,
                TransactionType = Domain.Enums.TransactionType.WITHDRAW,
                DateTransaction = DateTime.Now
            };

            _mockUnitOfWork.Setup(s => s.AccountRepository.SelectOne(It.IsAny<Expression<Func<Account, bool>>>()))
                           .Returns(FakeData.Account(AccountStatus.Inactive));

            _mockUnitOfWork.Setup(s => s.TransactionRepository.Insert(It.IsAny<Transaction>()));

            var handler = new WithDrawMoneyAccountRequestHandler(_mockUnitOfWork.Object, mediator, _mockLogger.Object);

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(request, CancellationToken.None));
            Assert.Contains("Account is Inactive!", ex.Message);
        }

        [Fact]
        public async void Should_Return_Exception_When_Account_Is_Not_Nound()
        {
            var _mockLoggerUpdateBalance = new Mock<ILogger<UpdateBalanceAccountRequestHandler>>();
            var serviceProvider = new ServiceCollection()
                .AddTransient(_ => _mockUnitOfWork.Object)
                .AddTransient(_ => _mockLoggerUpdateBalance.Object)
                .AddTransient<IRequestHandler<UpdateBalanceAccountRequest, Unit>, UpdateBalanceAccountRequestHandler>()
                .AddTransient<IMediator, Mediator>()
                .BuildServiceProvider();

            var mediator = serviceProvider.GetRequiredService<IMediator>();

            var request = new WithDrawMoneyAccountRequest
            {
                AccountNumber = 1,
                Amount = 110
            };

            var mockResponse = new TransactionViewModel
            {
                Amount = 10,
                TransactionType = Domain.Enums.TransactionType.WITHDRAW,
                DateTransaction = DateTime.Now
            };

            _mockUnitOfWork.Setup(s => s.AccountRepository.SelectOne(It.IsAny<Expression<Func<Account, bool>>>()))
                           .Returns((Account)null);

            _mockUnitOfWork.Setup(s => s.TransactionRepository.Insert(It.IsAny<Transaction>()));

            var handler = new WithDrawMoneyAccountRequestHandler(_mockUnitOfWork.Object, mediator, _mockLogger.Object);

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(request, CancellationToken.None));
            Assert.Contains("Account not found", ex.Message);
        }

    }
}
