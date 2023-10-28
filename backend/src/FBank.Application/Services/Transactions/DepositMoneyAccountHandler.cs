﻿using AutoMapper;
using FBank.Application.Interfaces;
using FBank.Application.Requests;
using FBank.Application.Requests.Transactions;
using FBank.Application.ViewMoldels;
using FBank.Domain.Entities;
using FBank.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FBank.Application.Services.Transactions
{
    public class DepositMoneyAccountHandler : IRequestHandler<DepositMoneyAccountRequest, TransactionViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DepositMoneyAccountHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public DepositMoneyAccountHandler(
            IMediator mediator,
            ILogger<DepositMoneyAccountHandler> logger,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<TransactionViewModel> Handle(DepositMoneyAccountRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Todo: Trocar esta verificação, por uma rotina de validação, quando a rotina de verifica se uma conta existe estiver pronta
                //Todo: Incluir método para verificar se a conta existe
                if (!VerifyValueDeposit(request.Value))
                {
                    var transactionViewModel = new TransactionViewModel();
                    //transactionViewModel.Id = Guid.Empty;
                    return await Task.FromResult(transactionViewModel);
                }

                var transactionBank = CompleteDataDeposit(request);
                _unitOfWork.TransactionRepository.Insert(transactionBank);
                var transactionReturn = _unitOfWork.TransactionRepository.SelectToId(transactionBank.Id);

                await _mediator.Send(new UpdateBalanceAccountRequest()
                {
                    AccountId = transactionBank.AccountId,
                    Value = transactionBank.Value,
                    FlowType = transactionBank.FlowType
                });
                _unitOfWork.Commit();

                var mappedResult = _mapper.Map<TransactionViewModel>(transactionReturn);
                return await Task.FromResult(mappedResult);
            }catch(Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.LogInformation(ex.ToString());
                throw new Exception("Erro ao efetuar deposito", ex);
            }
        }

        public Transaction CompleteDataDeposit(DepositMoneyAccountRequest request)
        {
            var account = VerifyAccountExists(request.Account);
            var transactionBank = new Transaction();
            transactionBank.TransactionType = TransactionType.DEPOSIT;
            transactionBank.FlowType = FlowType.INPUT;
            transactionBank.AccountToId= account.Id;
            transactionBank.AccountId = account.Id;
            transactionBank.Value= request.Value;
            return transactionBank;
        }

        public bool VerifyValueDeposit(decimal valueDeposit)
        {
            if (valueDeposit > 0)
                return true;
            else
                return false;
        }
        public Account VerifyAccountExists(int accountNumber)
        {
            var account = _unitOfWork.AccountRepository.SelectOne(x => x.Number == accountNumber);
            if (account == null)
                throw new Exception($"Conta não encontrada");
            return account;
        }
    }
}
