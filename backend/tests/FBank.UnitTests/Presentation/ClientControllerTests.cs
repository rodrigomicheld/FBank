using FBank.Application.Queries;
using FBank.Application.ViewMoldels;
using FBank.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FBank.UnitTests.Presentation
{
    public class ClientControllerTests
    {
        private Mock<IMediator> _mockMediator;
        private ClientController _clientController;

        public ClientControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _clientController = new ClientController(_mockMediator.Object);
        }

        [Fact]
        public void Should_return_client_requested()
        {
            _mockMediator.Setup(obj => obj.Send(It.IsAny<GetOneClientQuery>(), new CancellationToken())).ReturnsAsync(new ClientViewModel());

            var response = _clientController.GetOneAsync(It.IsAny<string>());
            
            Assert.NotNull(response);      
        }
    }
}
