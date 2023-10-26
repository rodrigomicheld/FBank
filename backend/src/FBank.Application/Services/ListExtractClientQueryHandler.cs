using FBank.Application.Dto;
using FBank.Application.Extensions;
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

        public ListExtractClientQueryHandler(ITransactionRepository transactionRepository, IAccountRepository accountRepository, ILogger<ListExtractClientQueryHandler> logger)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _logger = logger;
        }

        public async Task<PaginationResponse<ClientExtractViewModel>> Handle(ListExtractClientQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Listando extrato do cliente agência - {query.FilterClient.NumberAgency} / conta - {query.FilterClient.NumberAccount}");

            if (query.FilterClient.InitialDate > query.FilterClient.FinalDate || query.FilterClient.FinalDate < query.FilterClient.InitialDate)
                throw new ArgumentException();
            
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
                        Description = GetDescriptionTransaction(),
                        Amount = string.Empty,
                        Balance = GetBalanceAccount(extractClient.IdAccountOrigin).ToString()
                    });

                    viewModels.Add(new ClientExtractViewModel
                    {
                        DateTransaction = extractClient.DateTransaction,
                        Description = GetDescriptionTransaction(extractClient),
                        Amount = extractClient.Amount.ToString(),
                        Balance = string.Empty
                    });
                }
                else
                {
                    viewModels.Add(new ClientExtractViewModel
                    {
                        DateTransaction = extractClient.DateTransaction,
                        Description = GetDescriptionTransaction(extractClient),
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

        private string GetDescriptionTransaction(ClientExtractToListDto extractClient = null) 
        {
            switch (extractClient?.TransactionType)
            {
                case TransactionType.TRANSFER:
                    var accountDestination = _accountRepository.SelectOneColumn(x => x.Id == extractClient.IdAccountDestination, x => x.Client.Name);
                    var dateTransaction = extractClient.DateTransaction.ToString("MM-dd");
                    return $"{extractClient.IdTransaction.ToString().Substring(0, 7)} {EnumExtensions.GetDescription(extractClient.TransactionType)} {accountDestination.Substring(0, 6)}{dateTransaction}";

                case TransactionType.WITHDRAW:
                    return $"{extractClient.IdTransaction.ToString().Substring(0, 7)} {EnumExtensions.GetDescription(extractClient.TransactionType)}";

                case TransactionType.DEPOSIT:
                    return $"{extractClient.IdTransaction.ToString().Substring(0, 7)} {EnumExtensions.GetDescription(extractClient.TransactionType)}";

                case TransactionType.PAYMENT: 
                    return string.Empty;

                default:
                    return "SALDO TOTAL DISPONÍVEL DIA";



            }
        }
    }
}
