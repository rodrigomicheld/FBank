using FBank.Application.Dto;
using FBank.Application.Requests.Transactions;
using FBank.Application.ViewMoldels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FBank.Presentation.Controllers
{
    public class TransactionController : StandardController
    {
        public TransactionController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("WithDraw")]
        public async Task<ActionResult<TransactionViewModel>> PostTransactionWithDraw([FromBody] AmountDto dto)
        {
            var authorizationResult = CheckAccountClaim();
            if (authorizationResult == null)
                return Unauthorized("Unauthorized user");

            try
            {
                return Ok(await mediator.Send(new WithDrawMoneyAccountRequest 
                { 
                    AccountNumber = authorizationResult.Value,
                    Amount = dto.Value

                }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("DepositAccount")]
        public async Task<ActionResult<TransactionViewModel>> PostTransactionDeposit([FromBody] AmountDto dto)
        {
            var authorizationResult = CheckAccountClaim();
            if (authorizationResult == null)
                return Unauthorized("Unauthorized user");

            try
            {
                return Ok(await mediator.Send( new DepositMoneyAccountRequest
                {
                    AccountNumber = authorizationResult.Value,
                    AgencyCode = 1,
                    Value = dto.Value
                })); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("Transfer")]
        public async Task<ActionResult<string>> PostTransactionTransfer([FromBody] TransferMoneyAccountDto dto)
        {
            var authorizationResult = CheckAccountClaim();
            if (authorizationResult == null)
                return Unauthorized("Unauthorized user");

            try
            {
                return Ok(await mediator.Send(new TransferMoneyAccountRequest
                {
                    AccountNumberFrom = authorizationResult.Value,
                    AccountNumberTo = dto.AccountNumberTo,
                    Value = dto.Value,
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private int? CheckAccountClaim()
        {
            var user = HttpContext.User;

            if (!user.Identity.IsAuthenticated)
                return null;

            var documentClaim = user.Claims.FirstOrDefault(c => c.Type == "Account");

            if (documentClaim == null)
                return null;

            if (int.TryParse(documentClaim.Value, out int accountValue))
                return accountValue;

            return null;
        }
    }
}
