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
        public async Task<ActionResult<ClientViewModel>> PostTransactionWithDraw([FromBody] WithDrawMoneyAccountRequest  request)
        {
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
            try
            {
                return Ok(await mediator.Send(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
