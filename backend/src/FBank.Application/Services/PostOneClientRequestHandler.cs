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
        private readonly ILogger<PostOneClientRequestHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public PostOneClientRequestHandler(ILogger<PostOneClientRequestHandler> logger, IUnitOfWork unitOfWork)
        {
            _logger=logger;
            _unitOfWork=unitOfWork;
        }

        public Task<string> Handle(PostOneClientRequest request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Cadastrando o cliente: {request.Document}");

                var typeDocument = CpfCnpj.ValidTypeDocument(request.Document);

                if (typeDocument == PersonType.None)
                    throw new ArgumentException("Invalid document!");

                var client = _unitOfWork.ClientRepository.SelectOne(x => x.Document == request.Document);

                if (client != null)
                    throw new InvalidOperationException("Client already!");

                client = new Client
                {
                    Document = request.Document,
                    Name = request.Name,
                    DocumentType = typeDocument,
                    Password = request.Password,
                };

                _unitOfWork.ClientRepository.Insert(client);

                var agency = _unitOfWork.AgencyRepository.SelectOne(x => x.Code == 1);

                var account = new Account
                {
                    ClientId = client.Id,
                    AgencyId = agency.Id,
                    Status = AccountStatusEnum.Active,
                };

                _unitOfWork.AccountRepository.Insert(account);
                _unitOfWork.Commit();

                return Task.FromResult($"Agency: {agency.Code} - Account: {account.Number}");
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.LogInformation(ex.ToString());
                throw new Exception("Erro ao efetuar Cadastro", ex);
            }
        }
    }
}
