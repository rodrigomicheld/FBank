using Application.Interfaces;
using Application.Requests.Transactions;
using Domain.Enums;
using MediatR;

namespace Application.Services.Transactions
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
                errors.Add("Account not found");
            if (request.Value <= 0)
                errors.Add("Value to be updated cannot be less than or equal to zero");

            if (request.FlowType.GetHashCode() == FlowType.OUTPUT.GetHashCode() && request.Value > accountUpdated.Balance)
                errors.Add("Insufficient balance");

            if (errors.Count > 0)
                throw new Exception($"Error updating Balance, errors : {string.Join(",", errors)}");

            accountUpdated.Balance = accountUpdated.Balance +
                                    (request.FlowType.GetHashCode() == FlowType.OUTPUT.GetHashCode() ? request.Value * -1
                                    : request.Value);

            _unitOfWork.AccountRepository.Update(accountUpdated);

            return Unit.Value;
        }
    }
}
