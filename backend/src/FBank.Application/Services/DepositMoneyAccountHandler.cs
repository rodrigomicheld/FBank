using AutoMapper;
using FBank.Application.Commands;
using FBank.Application.Interfaces;
using FBank.Application.ViewMoldels;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBank.Application.Services
{
    public class DepositMoneyAccountHandler : IRequestHandler<DepositMoneyAccountCommand, TransactionViewModel>
    {

        private readonly ITransactionRepository _iTransactionRepository;
        private readonly ILogger<DepositMoneyAccountHandler> _logger;
        private readonly IMapper _mapper;

        public DepositMoneyAccountHandler(ITransactionRepository iTransactionRepository, ILogger<DepositMoneyAccountHandler> logger, IMapper mapper)
        {
            _iTransactionRepository = iTransactionRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public Task<TransactionViewModel> Handle(DepositMoneyAccountCommand request, CancellationToken cancellationToken)
        {
             _iTransactionRepository.Insert(request);
            var transactionViewModel = new TransactionViewModel();
            return Task.FromResult(transactionViewModel);
        }
    }
}
