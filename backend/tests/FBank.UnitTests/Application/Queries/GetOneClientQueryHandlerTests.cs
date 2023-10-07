using AutoMapper;
using FBank.Application.Interfaces;
using FBank.Application.Queries;
using FBank.Application.Services;
using FBank.Domain.Entities;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace FBank.UnitTests.Application.Queries
{
    public class GetOneClientQueryHandlerTests
    {
        
        private readonly IClientRepository _mockClientRepository;
        private readonly GetOneClientQuery _query;
        private readonly GetOneClientQueryHandler _handler;
        

        public GetOneClientQueryHandlerTests()
        {
            _mockClientRepository = Substitute.For<IClientRepository>();

            _mockClientRepository.SelectToId(Arg.Any<Guid>()).Returns(new Client());
            _query = new GetOneClientQuery();
            _handler = new GetOneClientQueryHandler(
                _mockClientRepository,
                Substitute.For<ILogger<GetOneClientQueryHandler>>(),
                Substitute.For<IMapper>());     
        }

        [Fact]
        public void Shoud_return_client_requested()
        {
            _mockClientRepository.SelectToId(Arg.Any<Guid>()).Returns(new Client());
            
            var response = _handler.Handle(_query, CancellationToken.None);

            Assert.NotNull(response);
        }

        [Fact]
        public void Shoud_return_NullReferenceException_when_client_not_found()
        {
            _mockClientRepository.SelectToId(Arg.Any<Guid>()).Throws(new NullReferenceException());
            Assert.ThrowsAsync<NullReferenceException>(() => _handler.Handle(_query, CancellationToken.None));
        }
    }
}
