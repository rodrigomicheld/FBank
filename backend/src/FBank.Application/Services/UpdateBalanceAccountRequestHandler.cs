using FBank.Application.Interfaces;
using FBank.Application.Requests;
using FBank.Application.ViewMoldels;
using FBank.Domain.Enums;
using MediatR;

namespace FBank.Application.Services
{
    public class UpdateBalanceAccountRequestHandler : IRequestHandler<UpdateBalanceAccountRequest, UpdateBalanceViewModel>
    {        
        private readonly IAccountRepository _accountRepository;

        public UpdateBalanceAccountRequestHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<UpdateBalanceViewModel> Handle(UpdateBalanceAccountRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //var account = request.Account;
                var accountUpdated = _accountRepository.SelectToId(request.AccountId);
                List<string> errors = new List<string>();
                if (accountUpdated == null)
                    errors.Add("Conta não encontrada");               
                if (request.Value <= 0)
                    errors.Add("Valor a ser atualizao não pode menor ou igual a zero");


                if (errors.Count > 0)
                    throw new Exception($"Erro ao atualizar Saldo, erros : {String.Join(",", errors)}");
               
                accountUpdated.Balance = accountUpdated.Balance +
                                         ((request.FlowType.GetHashCode() == FlowType.OUTPUT.GetHashCode()) ? (request.Value * -1)
                                         : request.Value);
                
                _accountRepository.Update(accountUpdated);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return new UpdateBalanceViewModel();       
        }
    }
}
