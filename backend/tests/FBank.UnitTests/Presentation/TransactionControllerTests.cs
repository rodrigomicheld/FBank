using FBank.Application.Queries;
using FBank.Application.Requests;
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
    }
}
