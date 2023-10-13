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
        public void Shoud_return_client_requested()
        {
            _mockMediator.Setup(obj => obj.Send(It.IsAny<GetOneClientQuery>(), new CancellationToken())).ReturnsAsync(new ClientViewModel());

            var response = _clientController.GetOneAsync(It.IsAny<string>());
            
            Assert.NotNull(response);      
        }

        [Fact]
        public void Shoud_return_bad_request_when_client_does_not_existAsync()
        {
            var result = _mockMediator.Setup(obj => obj.Send(It.IsAny<GetOneClientQuery>(), new CancellationToken())).Throws<Exception>();

            var response = _clientController.GetOneAsync(It.IsAny<string>()).Result;

            var resultado = response?.Result;

            Assert.Null(response?.Value);
            Assert.IsType<BadRequestObjectResult>(resultado);
        }
    }
}
