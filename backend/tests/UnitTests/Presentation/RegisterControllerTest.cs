using Application.Interfaces;
using Application.Requests.Accounts;
using Application.Services.Accounts;
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
            _handler = new PostOneClientRequestHandler(_logger, _mockUnitOfWork);
        }
        

    }
}

