using FBank.Application.Interfaces;
using FBank.Application.Requests;
using FBank.Domain.Entities;
using FBank.Domain.Enums;
using FBank.Domain.Validators;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FBank.Application.Services
{
    public class PostOneClientRequestHandler : IRequestHandler<PostOneClientRequest, Guid>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IAgencyRepository _agencyRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<PostOneClientRequestHandler> _logger;

        public PostOneClientRequestHandler(
            IClientRepository clientRepository, 
            ILogger<PostOneClientRequestHandler> logger,
            IAgencyRepository agencyRepository,
            IAccountRepository accountRepository)
        {
            _clientRepository = clientRepository;
            _logger = logger;
            _agencyRepository = agencyRepository;
            _accountRepository = accountRepository;
        }

        public Task<string> Handle(PostOneClientQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Cadastrando o cliente: {request.Document}");

            var typeDocument = CpfCnpj.ValidTypeDocument(request.Document);

            if(typeDocument == PersonType.None)
                throw new ArgumentException("Invalid document!");

            var client = _clientRepository.SelectOne(x=> x.Document == request.Document);

            if (client != null && client.Accounts != null && 
                client.Accounts.Any(x=> x.Status == AccountStatusEnum.Active))
                throw new InvalidOperationException("Client already has an active account!");

            if (client == null)
            {
                client = new Client
                {
                    Document = request.Document,
                    Name = request.Name,
                    DocumentType = typeDocument,
                    Password = request.Password,
                };

                _clientRepository.Insert(client);
            }
            else
            {
                client.Password = request.Password;
                client.Name = request.Name;
                _clientRepository.Update(client);
            }

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
