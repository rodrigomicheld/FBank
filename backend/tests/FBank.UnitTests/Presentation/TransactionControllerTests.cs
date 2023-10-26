using AutoMapper.Features;
using FBank.Application.Requests;
using FBank.Application.ViewMoldels;
using FBank.Domain.Entities;
using FBank.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSubstitute;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Principal;
namespace FBank.UnitTests.Presentation
{
    public class TransactionControllerTests
    {
        private Mock<IMediator> _mockMediator;
        private TransactionController _transactionController;

        public TransactionControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _transactionController = new TransactionController(_mockMediator.Object);
        }

        [Fact]
        public void Should_return_transaction_requested()
        {
            _mockMediator.Setup(obj => obj.Send(It
                                .IsAny<DepositMoneyAccountRequest>(), new CancellationToken()))
                                .ReturnsAsync(new TransactionViewModel());
            var response = _transactionController.PostTransactionDeposit(It.IsAny<DepositMoneyAccountRequest>());
            Assert.NotNull(response);
        }

        [Fact]
        public void Should_return_bad_request_when_transaction_does_not_didAsync()
        {
            var depositMoneyAccountRequest = new DepositMoneyAccountRequest();
            var result = _mockMediator.Setup(obj => obj.Send(It.IsAny<DepositMoneyAccountRequest>(), new CancellationToken())).Throws<Exception>();
            var response = _transactionController.PostTransactionDeposit(depositMoneyAccountRequest).Result;
            var resultado = response?.Result;

            Assert.Null(response?.Value);
            Assert.IsType<BadRequestObjectResult>(resultado);
        }

        [Fact]
        public async void Should_Make_The_WithdrawalAsync()
        {
            // Arrange
            _transactionController.ControllerContext = new ControllerContext();
            _transactionController.ControllerContext.HttpContext = FakeData.ContextRequestWithLogin();

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

            _mockMediator.Setup(obj => obj.Send(It
                               .IsAny<WithDrawMoneyAccountRequest>(), new CancellationToken()))
                               .ReturnsAsync(mockResponse);

            // Act
            var response = await _transactionController.PostTransactionWithDraw(request);

            // Assert
            Assert.NotNull(response.Value);
            Assert.Equal(Domain.Enums.TransactionType.SAQUE, response.Value.TransactionType);
            Assert.Equal(mockResponse.DateTransaction, response.Value.DateTransaction);
            Assert.Equal(10, response.Value.Amount);
        }
    }
}
