using AutoMapper;
using FBank.Application.Interfaces;
using FBank.Application.Queries;
using FBank.Application.ViewMoldels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FBank.Application.Services
{
    public class GetOneClientQueryHandler : IRequestHandler<GetOneClientQuery, ClientViewModel>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<GetOneClientQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetOneClientQueryHandler(IClientRepository clientRepository, ILogger<GetOneClientQueryHandler> logger, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public Task<ClientViewModel> Handle(GetOneClientQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Listando o cliente Documento: {request.Document}");

           
             var queryResult = _clientRepository.SelectOne(x=> x.Document == request.Document);
           
            if (queryResult == null)
                throw new NullReferenceException("Client not found!");
            
            var mappedResult = _mapper.Map<ClientViewModel>(queryResult);

            return Task.FromResult(mappedResult);
          
        }
    }
}
