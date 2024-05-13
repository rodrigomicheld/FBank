using Application.Interfaces;
using Application.Requests.Accounts;
using Application.Requests.Transactions;
using Application.Services.Accounts;
using Application.Services.Transactions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace UnitTests.Presentation
{
    public class RegisterControllerTest
    {
        private readonly PostOneClientRequest _query;
        private readonly PostOneClientRequestHandler _handler;
        private readonly IMediator _mockMediator;
        private readonly IUnitOfWork _mockUnitOfWork;
        private readonly ILogger<PostOneClientRequestHandler> _logger;

        public RegisterControllerTest()
        {
            _mockUnitOfWork = Substitute.For<IUnitOfWork>();
            _mockMediator = Substitute.For<IMediator>();

            _logger = Substitute.For<ILogger<PostOneClientRequestHandler>>();
            _query = new PostOneClientRequest();
            _handler = new PostOneClientRequestHandler(_logger,_mockUnitOfWork);
        }

        [Fact]
        public void Should_Return_Client_Registerd() 
        {
            var request = new PostOneClientRequest
            {
                Document = "31738918050",
                Name = "Teste de Souza",
                Password = "123456"
            };

            var response = _handler.Handle(request, CancellationToken.None);

            Assert.NotNull(response);

        }

    }
}

