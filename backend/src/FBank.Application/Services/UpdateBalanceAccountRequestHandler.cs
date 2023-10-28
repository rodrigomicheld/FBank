using FBank.Application.Interfaces;
using FBank.Application.Requests;
using FBank.Domain.Enums;
using MediatR;

namespace FBank.Application.Services
{
    public class UpdateBalanceAccountRequestHandler : IRequestHandler<UpdateBalanceAccountRequest, Unit>
    {
        IUnitOfWork _unitOfWork;
        public UpdateBalanceAccountRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateBalanceAccountRequest request, CancellationToken cancellationToken)
        {
            var accountUpdated = _unitOfWork.AccountRepository.SelectToId(request.AccountId);
            List<string> errors = new List<string>();
            if (accountUpdated == null)
                errors.Add("Conta não encontrada");
            if (request.Value <= 0)
                errors.Add("Valor a ser atualizao não pode menor ou igual a zero");

            if (request.FlowType.GetHashCode() == FlowType.OUTPUT.GetHashCode() && request.Value > accountUpdated.Balance)
                errors.Add("Saldo insuficiente");

            if (errors.Count > 0)
                throw new Exception($"Erro ao atualizar Saldo, erros : {String.Join(",", errors)}");

            accountUpdated.Balance = accountUpdated.Balance +
                                    ((request.FlowType.GetHashCode() == FlowType.OUTPUT.GetHashCode()) ? (request.Value * -1)
                                    : request.Value);

            _unitOfWork.AccountRepository.Update(accountUpdated);

            return Unit.Value;
        }
    }
}
