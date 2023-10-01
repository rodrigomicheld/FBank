using FBank.Application.Queries;
using FBank.Application.ViewMoldels;
using FBank.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
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

            var response = _clientController.GetOneAsync(It.IsAny<Guid>());
            
            Assert.NotNull(response);      
        }

        [Fact]
        public void Shoud_return_bad_request_when_client_does_not_exist()
        {
            var id = new Guid();
            var result = _mockMediator.Setup(obj => obj.Send(It.IsAny<GetOneClientQuery>(), new CancellationToken())).Throws<Exception>();

            var response = _clientController.GetOneAsync(id).Result;

            ActionResult<ClientViewModel> responseResult = response;
            //Assert.IsType<BadRequest>(response);

            //ActionResult responseResult = response.Result;

            //Assert.Equal(400, responseResult.Result);
            Assert.IsType<BadRequest>(responseResult);
            Assert.Null(response.Value);

            //  Assert.ThrowsAnyAsync<NullReferenceException>(() => _clientController.GetOneAsync(It.IsAny<Guid>()));

            ////  Assert.Equal(404, )

             
            //  Assert.IsType<BadRequestResult>(_clientController.Response.StatusCode);

        }
    }
}
