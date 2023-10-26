using AutoMapper;
using FBank.Application.Interfaces;
using FBank.Application.Requests;
using FBank.Application.Services;
using FBank.Application.ViewMoldels;
using FBank.Domain.Entities;
using FBank.Domain.Enums;
using FBank.Infrastructure;
using FBank.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Linq.Expressions;
using System.Security.Cryptography.Xml;

namespace FBank.UnitTests.Application.Requests
{
    public class WithDrawMoneyAccountRequestHandlerTests
    {
        //private readonly DepositMoneyAccountRequest _query;
        //private readonly WithDrawMoneyAccountRequestHandler _handler;
        //private readonly IMediator _mockMediator;
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
            // Arrange
            var _mockLoggerUpdateBalance = new Mock<ILogger<UpdateBalanceAccountRequestHandler>>();
            var serviceProvider = new ServiceCollection()
                .AddTransient(_=> _mockUnitOfWork.Object) 
                .AddTransient(_=> _mockLoggerUpdateBalance.Object) 
                .AddTransient<IRequestHandler<UpdateBalanceAccountRequest, UpdateBalanceViewModel>, UpdateBalanceAccountRequestHandler>()
                .AddTransient<IMediator, Mediator>()
                .BuildServiceProvider();

            var mediator = serviceProvider.GetRequiredService<IMediator>();

            var accountId = new Guid();
            var request = new WithDrawMoneyAccountRequest
            {
                AccountOrigin = accountId,
                Amount = 110
            };

            var mockResponse = new TransactionViewModel
            {
                Amount = 10,
                TransactionType = Domain.Enums.TransactionType.SAQUE,
                DateTransaction = DateTime.Now
            };

            _mockUnitOfWork.Setup(s => s.AccountRepository.SelectToId(It.IsAny<Guid>()))
                    .Returns(FakeData.Account());

            _mockUnitOfWork.Setup(s => s.TransactionRepository.Insert(It.IsAny<TransactionBank>()));

            var handler = new WithDrawMoneyAccountRequestHandler(_mockUnitOfWork.Object, mediator, _mockLogger.Object);

            var ex = await Assert.ThrowsAsync<Exception>(() => handler.Handle(request, CancellationToken.None));
            Assert.Contains("Erro ao alterar saldo", ex.Message);
            Assert.Contains("Saldo insuficiente", ex.InnerException.Message);
        }

        [Fact]
        public async void Should_Make_Withdrawal_Balance_Is_Greater()
        {
            // Arrange
            var _mockLoggerUpdateBalance = new Mock<ILogger<UpdateBalanceAccountRequestHandler>>();
            var serviceProvider = new ServiceCollection()
                .AddTransient(_ => _mockUnitOfWork.Object)
                .AddTransient(_ => _mockLoggerUpdateBalance.Object)
                .AddTransient<IRequestHandler<UpdateBalanceAccountRequest, UpdateBalanceViewModel>, UpdateBalanceAccountRequestHandler>()
                .AddTransient<IMediator, Mediator>()
                .BuildServiceProvider();

            var mediator = serviceProvider.GetRequiredService<IMediator>();

            var accountId = new Guid();
            var request = new WithDrawMoneyAccountRequest
            {
                AccountOrigin = accountId,
                Amount = 10
            };

            var mockResponse = new TransactionViewModel
            {
                Amount = 10,
                TransactionType = Domain.Enums.TransactionType.SAQUE,
                DateTransaction = DateTime.Now
            };

            _mockUnitOfWork.Setup(s => s.AccountRepository.SelectToId(It.IsAny<Guid>()))
                    .Returns(FakeData.Account());

            _mockUnitOfWork.Setup(s => s.TransactionRepository.Insert(It.IsAny<TransactionBank>()));

            var handler = new WithDrawMoneyAccountRequestHandler(_mockUnitOfWork.Object, mediator, _mockLogger.Object);

            var response = await handler.Handle(request, CancellationToken.None);

            Assert.Equal(10, response.Amount);
            Assert.Equal(TransactionType.SAQUE, response.TransactionType);
        }
    }
}
