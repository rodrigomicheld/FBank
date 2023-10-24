using FBank.Application.Dto;
using FBank.Application.Requests;
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

        [HttpPost]        
        public async Task<ActionResult<ClientViewModel>> PostTransactionWithDraw([FromBody] WithDrawMoneyAccountRequest request)
        {
            var authorizationResult = CheckAccountClaim();
            if (authorizationResult == null)
            {
                return Unauthorized("Unauthorized user");
            }


            try
            {
                return Ok(await mediator.Send(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("DepositAccount")]
        public async Task<ActionResult<TransactionViewModel>> PostTransactionDeposit([FromBody] DepositMoneyAccountRequest request)
        {
            var authorizationResult = CheckAccountClaim();
            if (authorizationResult == null)
            {
                return Unauthorized("Unauthorized user");
            }

            try
            {
                return Ok(await mediator.Send(request)); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("Transfer")]
        public async Task<ActionResult<TransferViewModel>> PostTransactionTransfer([FromBody] TransferMoneyAccountDto dto)
        {
            var authorizationResult = CheckAccountClaim();
            if (authorizationResult == null)
            {
                return Unauthorized("Unauthorized user");
            }

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
            {
                return null;
            }

            var documentClaim = user.Claims.FirstOrDefault(c => c.Type == "Account");

            if (documentClaim == null)
            {
                return null;
            }

            if (int.TryParse(documentClaim.Value, out int accountValue))
            {
                return accountValue;
            }

            return null;
        }
    }
}
