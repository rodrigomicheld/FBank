using Application.Interfaces;
using Application.Requests.Transactions;
using Application.Services.Transactions;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests.Application.Requests
{
    public class UpdateBalanceAccountRequestHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ILogger<WithDrawMoneyAccountRequestHandler>> _mockLogger;
        public UpdateBalanceAccountRequestHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockLogger = new Mock<ILogger<WithDrawMoneyAccountRequestHandler>>();
        }

        [Fact]
        public async void Should_Remove_The_Desired_Value_When_Executing_The_Command()
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient(_ => _mockUnitOfWork.Object)
                .AddTransient<IRequestHandler<UpdateBalanceAccountRequest, Unit>, UpdateBalanceAccountRequestHandler>()
                .AddTransient<IMediator, Mediator>()

                .BuildServiceProvider();

            var mediator = serviceProvider.GetRequiredService<IMediator>();

            var request = new UpdateBalanceAccountRequest
            {
                AccountId = Guid.NewGuid(),
                FlowType = FlowType.OUTPUT,
                Value = 10
            };

            var fakeAccount = FakeData.Account();

            _mockUnitOfWork.Setup(s => s.AccountRepository.SelectToId(It.IsAny<Guid>()))
                    .Returns(fakeAccount);

            _mockUnitOfWork.Setup(s => s.AccountRepository.Update(It.IsAny<Account>()));

            var handler = new UpdateBalanceAccountRequestHandler(_mockUnitOfWork.Object);

            Assert.Equal(100M, _mockUnitOfWork.Object.AccountRepository.SelectToId(fakeAccount.Id).Balance);
            var response = await handler.Handle(request, CancellationToken.None);

            Assert.Equal(90M, _mockUnitOfWork.Object.AccountRepository.SelectToId(fakeAccount.Id).Balance);
        }

        [Fact]
        public async void Should_Add_The_Desired_Value_When_Executing_The_Command()
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient(_ => _mockUnitOfWork.Object)
                .AddTransient<IRequestHandler<UpdateBalanceAccountRequest, Unit>, UpdateBalanceAccountRequestHandler>()
                .AddTransient<IMediator, Mediator>()
                .BuildServiceProvider();

            var mediator = serviceProvider.GetRequiredService<IMediator>();

            var request = new UpdateBalanceAccountRequest
            {
                AccountId = Guid.NewGuid(),
                FlowType = FlowType.INPUT,
                Value = 10
            };

            var fakeAccount = FakeData.Account();

            _mockUnitOfWork.Setup(s => s.AccountRepository.SelectToId(It.IsAny<Guid>()))
                    .Returns(fakeAccount);

            _mockUnitOfWork.Setup(s => s.AccountRepository.Update(It.IsAny<Account>()));

            var handler = new UpdateBalanceAccountRequestHandler(_mockUnitOfWork.Object);

            Assert.Equal(100M, _mockUnitOfWork.Object.AccountRepository.SelectToId(fakeAccount.Id).Balance);
            var response = await handler.Handle(request, CancellationToken.None);

            Assert.Equal(110M, _mockUnitOfWork.Object.AccountRepository.SelectToId(fakeAccount.Id).Balance);
        }

        [Fact]
        public async void Should_Return_Exception_When_Balance_Is_Insufficient()
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient(_ => _mockUnitOfWork.Object)
                .AddTransient<IRequestHandler<UpdateBalanceAccountRequest, Unit>, UpdateBalanceAccountRequestHandler>()
                .AddTransient<IMediator, Mediator>()

                .BuildServiceProvider();

            var mediator = serviceProvider.GetRequiredService<IMediator>();

            var request = new UpdateBalanceAccountRequest
            {
                AccountId = Guid.NewGuid(),
                FlowType = FlowType.OUTPUT,
                Value = 110
            };

            var fakeAccount = FakeData.Account();

            _mockUnitOfWork.Setup(s => s.AccountRepository.SelectToId(It.IsAny<Guid>()))
                    .Returns(fakeAccount);

            _mockUnitOfWork.Setup(s => s.AccountRepository.Update(It.IsAny<Account>()));

            var handler = new UpdateBalanceAccountRequestHandler(_mockUnitOfWork.Object);

            Assert.Equal(100M, _mockUnitOfWork.Object.AccountRepository.SelectToId(fakeAccount.Id).Balance);

            var ex = await Assert.ThrowsAsync<Exception>(() => handler.Handle(request, CancellationToken.None));
            Assert.Contains("Insufficient balance", ex.Message);
        }

        [Fact]
        public async void Should_Return_Exception_When_Account_Is_Not_Nound_Or_Operation_Value_Is_0()
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient(_ => _mockUnitOfWork.Object)
                .AddTransient<IRequestHandler<UpdateBalanceAccountRequest, Unit>, UpdateBalanceAccountRequestHandler>()
                .AddTransient<IMediator, Mediator>()

                .BuildServiceProvider();

            var mediator = serviceProvider.GetRequiredService<IMediator>();

            var request = new UpdateBalanceAccountRequest
            {
                AccountId = Guid.NewGuid(),
                FlowType = FlowType.OUTPUT,
                Value = 0
            };

            _mockUnitOfWork.Setup(s => s.AccountRepository.SelectToId(It.IsAny<Guid>()))
                    .Returns((Account)null);

            _mockUnitOfWork.Setup(s => s.AccountRepository.Update(It.IsAny<Account>()));

            var handler = new UpdateBalanceAccountRequestHandler(_mockUnitOfWork.Object);

            var ex = await Assert.ThrowsAsync<Exception>(() => handler.Handle(request, CancellationToken.None));
            Assert.Contains("Account not found", ex.Message);
            Assert.Contains("Value to be updated cannot be less than or equal to zero", ex.Message);
        }
    }
}
