using FBank.Application.Interfaces;
using FBank.Application.Requests.Accounts;
using FBank.Application.Services.Accounts;
using FBank.Domain.Entities;
using FBank.Domain.Enums;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using System.Linq.Expressions;

namespace FBank.UnitTests.Application.Requestsr
{
    public class AccountStatusRequestHandlerTests
    {
        private readonly ILogger<AccountStatusRequestHandler> _mockLogger;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public AccountStatusRequestHandlerTests()
        {
            _mockLogger = Substitute.For<ILogger<AccountStatusRequestHandler>>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public void Should_return_InvalidOperationException_when_account_not_exit()
        {
            var request = new AccountStatusRequest { AccountNumber = 1, AccountStatus = AccountStatus.Active };
            Account account = null;
            _mockUnitOfWork.Setup(s => s.AccountRepository.SelectOne(It.IsAny<Expression<Func<Account, bool>>>()))
                    .Returns(account);

            var handler = new AccountStatusRequestHandler(_mockLogger, _mockUnitOfWork.Object);

            var exception = Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(request, CancellationToken.None)).Result;

            Assert.Equal("Account does not exist!", exception.Message);
        }

        [Fact]
        public void Should_return_InvalidOperationException_when_account_already_has_this_status()
        {
            var request = new AccountStatusRequest { AccountNumber = 1, AccountStatus = AccountStatus.Active };

            var account = new Account
            {
                Id = Guid.NewGuid(),
                Number = request.AccountNumber,
                Status = request.AccountStatus
            };

            _mockUnitOfWork.Setup(s => s.AccountRepository.SelectOne(It.IsAny<Expression<Func<Account, bool>>>()))
                    .Returns(account);

            var handler = new AccountStatusRequestHandler(_mockLogger, _mockUnitOfWork.Object);

            var exception = Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(request, CancellationToken.None)).Result;

            Assert.Equal($"Account is {request.AccountStatus}!", exception.Message);
        }

        [Fact]
        public void Should_update_account_status_active()
        {
            var request = new AccountStatusRequest { AccountNumber = 1, AccountStatus = AccountStatus.Active };

            var account = new Account
            {
                Id = Guid.NewGuid(),
                Number = request.AccountNumber,
                Status = AccountStatus.Inactive
            };

            _mockUnitOfWork.Setup(s => s.AccountRepository.SelectOne(It.IsAny<Expression<Func<Account, bool>>>()))
                    .Returns(account);

            var handler = new AccountStatusRequestHandler(_mockLogger, _mockUnitOfWork.Object);

            var response = handler.Handle(request, CancellationToken.None).Result;

            Assert.Equal($"Account {account.Status} is successfully.", response);
        }

        [Fact]
        public void Should_update_account_status_inactive()
        {
            var request = new AccountStatusRequest { AccountNumber = 1, AccountStatus = AccountStatus.Inactive };

            var account = new Account
            {
                Id = Guid.NewGuid(),
                Number = request.AccountNumber,
                Status = AccountStatus.Active
            };

            _mockUnitOfWork.Setup(s => s.AccountRepository.SelectOne(It.IsAny<Expression<Func<Account, bool>>>())).Returns(account);

            var handler = new AccountStatusRequestHandler(_mockLogger, _mockUnitOfWork.Object);

            var response = handler.Handle(request, CancellationToken.None).Result;

            Assert.Equal($"Account {account.Status} is successfully.", response);
        }
    }
}
