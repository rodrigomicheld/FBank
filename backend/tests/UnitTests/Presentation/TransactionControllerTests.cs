using Application.Dto;
using Application.Requests.Transactions;
using Application.ViewMoldels;
using Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
namespace UnitTests.Presentation
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
            var response = _transactionController.PostTransactionDeposit(It.IsAny<AmountDto>());
            Assert.NotNull(response);
        }

        [Fact]
        public void Should_return_bad_request_when_transaction_does_not_didAsync()
        {
            var depositMoneyAccountRequest = new AmountDto();
            var result = _mockMediator.Setup(obj => obj.Send(It.IsAny<DepositMoneyAccountRequest>(), new CancellationToken())).Throws<Exception>();

            _transactionController.ControllerContext = new ControllerContext();
            _transactionController.ControllerContext.HttpContext = FakeData.ContextRequestWithLogin();

            var response = _transactionController.PostTransactionDeposit(depositMoneyAccountRequest).Result;
            var resultado = response?.Result;

            Assert.Null(response?.Value);
            Assert.IsType<BadRequestObjectResult>(resultado);
        }

        [Fact]
        public async void Should_Make_The_WithdrawalAsync()
        {
            _transactionController.ControllerContext = new ControllerContext();
            _transactionController.ControllerContext.HttpContext = FakeData.ContextRequestWithLogin();

            var request = new AmountDto
            {
                Value = 10
            };

            var mockResponse = new TransactionViewModel
            {
                Amount = 10,
                TransactionType = Domain.Enums.TransactionType.WITHDRAW,
                DateTransaction = DateTime.Now
            };

            _mockMediator.Setup(obj => obj.Send(It
                               .IsAny<WithDrawMoneyAccountRequest>(), new CancellationToken()))
                               .ReturnsAsync(mockResponse);

            var response = await _transactionController.PostTransactionWithDraw(request);

            TransactionViewModel transactionViewModel = new TransactionViewModel();
            if (response.Result is OkObjectResult okResult)
            {
                transactionViewModel = okResult.Value as TransactionViewModel;
            }
            
            Assert.Equal(Domain.Enums.TransactionType.WITHDRAW, transactionViewModel.TransactionType);
            Assert.Equal(mockResponse.DateTransaction, transactionViewModel.DateTransaction);
            Assert.Equal(10, transactionViewModel.Amount);
        }
    }
}
