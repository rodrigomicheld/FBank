using FBank.Application.Requests.Transactions;
using FBank.Application.ViewMoldels;
using FBank.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

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
    }
}
