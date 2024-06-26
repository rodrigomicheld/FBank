﻿using AutoMapper;
using Application.Interfaces;
using Application.Queries.Accounts;
using Application.ViewMoldels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Accounts
{
    public class GetOneClientQueryHandler : IRequestHandler<GetOneClientQuery, ClientViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetOneClientQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetOneClientQueryHandler(IUnitOfWork unitOfWork, ILogger<GetOneClientQueryHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public Task<ClientViewModel> Handle(GetOneClientQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Listando o cliente Documento: {request.Document}");

            var queryResult = _unitOfWork.ClientRepository.SelectOne(x => x.Document == request.Document);

            if (queryResult == null)
                throw new NullReferenceException("Client not found!");

            var mappedResult = _mapper.Map<ClientViewModel>(queryResult);

            return Task.FromResult(mappedResult);
        }
    }
}
