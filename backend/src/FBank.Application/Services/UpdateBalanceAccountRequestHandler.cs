using FBank.Application.Interfaces;
using FBank.Application.Requests;
using FBank.Application.ViewMoldels;
using FBank.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FBank.Application.Services
{
    public class UpdateBalanceAccountRequestHandler : IRequestHandler<UpdateBalanceAccountRequest, UpdateBalanceViewModel>
    {        
        IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        public UpdateBalanceAccountRequestHandler(IUnitOfWork unitOfWork, ILogger<UpdateBalanceAccountRequestHandler> logger)
        {
            _unitOfWork=unitOfWork;
            _logger=logger;
        }

        public async Task<UpdateBalanceViewModel> Handle(UpdateBalanceAccountRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var accountUpdated = _unitOfWork.AccountRepository.SelectToId(request.AccountId);
                List<string> errors = new List<string>();
                if (accountUpdated == null)
                    errors.Add("Conta não encontrada");               
                if (request.Value <= 0)
                    errors.Add("Valor a ser atualizao não pode menor ou igual a zero");

                if (request.FlowType.GetHashCode() == FlowType.SAIDA.GetHashCode() && request.Value > accountUpdated.Balance)
                    errors.Add("Saldo insuficiente");
                
                if (errors.Count > 0)
                    throw new Exception($"Erro ao atualizar Saldo, erros : {String.Join(",", errors)}");


                accountUpdated.Balance = accountUpdated.Balance +
                                         ((request.FlowType.GetHashCode() == FlowType.SAIDA.GetHashCode()) ? (request.Value * -1)
                                         : request.Value);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.LogInformation(ex.ToString());
                throw new Exception("Erro ao alterar saldo", ex);
            }
            return new UpdateBalanceViewModel();       
        }
    }
}
