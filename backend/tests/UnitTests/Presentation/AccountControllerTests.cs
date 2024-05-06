using Application.Queries.Accounts;
using Application.ViewMoldels;
using Presentation.Controllers;
using MediatR;
using Moq;

namespace UnitTests.Presentation
{
    public class AccountControllerTests
    {
        private Mock<IMediator> _mockMediator;
        private AccountController _accountController;

        public AccountControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _accountController = new AccountController(_mockMediator.Object);
        }

        [Fact]
        public void Should_return_client_requested()
        {
            _mockMediator.Setup(obj => obj.Send(It.IsAny<GetOneClientQuery>(), new CancellationToken())).ReturnsAsync(new ClientViewModel());

            var response = _accountController.GetOneAsync();
            
            Assert.NotNull(response);      
        }
    }
}
