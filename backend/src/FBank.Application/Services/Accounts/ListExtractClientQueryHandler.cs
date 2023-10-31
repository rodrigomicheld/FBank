using FBank.Application.Dto;
using FBank.Application.Extensions;
using FBank.Application.Interfaces;
using FBank.Application.Queries.Accounts;
using FBank.Application.ViewMoldels;
using FBank.Domain.Common;
using FBank.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FBank.Application.Services.Accounts
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
            foreach (var extractClient in paginationResponse.Data)
            {
                viewModels.Add(new ClientExtractViewModel
                {
                    DateTransaction = extractClient.DateTransaction.Date,
                    Description = GetDescriptionTransaction(extractClient),
                    Amount = extractClient.Amount.ToString(),
                });
            }

            return new PaginationResponse<ClientExtractViewModel>(viewModels, paginationResponse.TotalItems, paginationResponse.CurrentPage, query.FilterClient._size);
        }

        private string GetDescriptionTransaction(ClientExtractToListDto extractClient = null)
        {
            switch (extractClient?.TransactionType)
            {
                case TransactionType.TRANSFER:
                    var accountDestination = _accountRepository.SelectOneColumn(x => x.Id == extractClient.IdAccountDestination, x => x.Client.Name);

                    return $"{extractClient.IdTransaction.ToString().Substring(0, 7)} {EnumExtensions.GetDescription(extractClient.TransactionType)} {accountDestination}";
                case TransactionType.WITHDRAW:
                    return $"{extractClient.IdTransaction.ToString().Substring(0, 7)} {EnumExtensions.GetDescription(extractClient.TransactionType)}";

                case TransactionType.DEPOSIT:
                    return $"{extractClient.IdTransaction.ToString().Substring(0, 7)} {EnumExtensions.GetDescription(extractClient.TransactionType)}";

                case TransactionType.PAYMENT:
                    return string.Empty;

                default:
                    return string.Empty;
            }
        }
    }
}
