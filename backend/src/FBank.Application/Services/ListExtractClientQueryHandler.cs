using FBank.Application.Interfaces;
using FBank.Application.Queries;
using FBank.Application.ViewMoldels;
using FBank.Domain.Common;
using FBank.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FBank.Application.Services
{
    public class ListExtractClientQueryHandler : IRequestHandler<ListExtractClientQuery, PaginationResponse<ClientExtractViewModel>>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<ListExtractClientQueryHandler> _logger;

        public async Task<PaginationResponse<ClientExtractViewModel>> Handle(ListExtractClientQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Listando extrato do cliente agência - {query.FilterClient.ClientAgency} / conta - {query.FilterClient.ClientAccount}");

            var paginationResponse = await _transactionRepository.SelectManyWithFilterToList(query.FilterClient);

            var viewModels = new List<ClientExtractViewModel>();
            DateTime? dateBase = null;
            foreach (var extractClient in paginationResponse.Data)
            {
                if (extractClient.DateTransaction != dateBase)
                {
                    dateBase = extractClient.DateTransaction;

                    viewModels.Add(new ClientExtractViewModel
                    {
                        DateTransaction = (DateTime)dateBase,
                        Description = "SALDO TOTAL DISPONÍVEL DIA",
                        Amount = string.Empty,
                        Balance = GetBalanceAccount(extractClient.IdAccountOrigin).ToString()
                    });

                    viewModels.Add(new ClientExtractViewModel
                    {
                        DateTransaction = extractClient.DateTransaction,
                        Description = GetDescription(extractClient.IdTransaction, extractClient.TransactionType, extractClient.IdAccountDestination),
                        Amount = extractClient.Amount.ToString(),
                        Balance = string.Empty
                    });
                }
                else
                {
                    viewModels.Add(new ClientExtractViewModel
                    {
                        DateTransaction = extractClient.DateTransaction,
                        Description = GetDescriptionTransaction(extractClient.IdTransaction, extractClient.TransactionType, extractClient.IdAccountDestination),
                        Amount = extractClient.Amount.ToString(),
                        Balance = string.Empty
                    });
                }
            }

            return new PaginationResponse<ClientExtractViewModel>(viewModels, paginationResponse.TotalItems, paginationResponse.CurrentPage, query.FilterClient._size);
        }

        private string GetBalanceAccount(Guid idAccountOrigin)
        {
            return _accountRepository.SelectOneColumn(c => c.Id == idAccountOrigin, c => c.Balance).ToString();
        }

        private string GetDescriptionTransaction(Guid idTransaction, TransactionType transactionType, Guid idAccountDestination)
        {
            return $"{idTransaction} {GetDescription(transactionType)}";
        }
    }
}
