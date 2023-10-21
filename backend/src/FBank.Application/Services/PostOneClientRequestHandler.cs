using FBank.Application.Interfaces;
using FBank.Application.Requests;
using FBank.Domain.Entities;
using FBank.Domain.Enums;
using FBank.Domain.Validators;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FBank.Application.Services
{
    public class PostOneClientRequestHandler : IRequestHandler<PostOneClientRequest, string>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IAgencyRepository _agencyRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<PostOneClientRequestHandler> _logger;

        public PostOneClientRequestHandler(IClientRepository clientRepository, IAgencyRepository agencyRepository, IAccountRepository accountRepository, ILogger<PostOneClientRequestHandler> logger)
        {
            _clientRepository = clientRepository;
            _agencyRepository = agencyRepository;
            _accountRepository = accountRepository;
            _logger = logger;
        }

        public Task<string> Handle(PostOneClientRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Cadastrando o cliente: {request.Document}");

            var typeDocument = CpfCnpj.ValidTypeDocument(request.Document);

            if (typeDocument == PersonType.None)
                throw new ArgumentException("Invalid document!");

            var client = _clientRepository.SelectOne(x => x.Document == request.Document);

            if (client != null)
                throw new InvalidOperationException("Client already!");

            client = new Client
            {
                Document = request.Document,
                Name = request.Name,
                DocumentType = typeDocument,
                Password = request.Password,
            };

            _clientRepository.Insert(client);

            var agency = _agencyRepository.SelectOne(x => x.Code == 1);

            var account = new Account
            {
                ClientId = client.Id,
                AgencyId = agency.Id,
                Status = AccountStatusEnum.Active,
            };

            _accountRepository.Insert(account);

            return Task.FromResult($"Agency: {agency.Code} - Account: {account.Number}");
        }
    }
}
