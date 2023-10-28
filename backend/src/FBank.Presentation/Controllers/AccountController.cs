using FBank.Application.Queries;
using FBank.Application.Requests;
using FBank.Application.ViewMoldels;
using FBank.Domain.Common.Filters;
using FBank.Domain.Common;
using FBank.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FBank.Presentation.Controllers
{
    public class AccountController : StandardController
    {
        public AccountController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPut("active-account")]
        public async Task<IActionResult> ActiveAsync()
        {
            var authorizationResult = CheckAccountClaim();
            if (authorizationResult == null)
                return Unauthorized("Unauthorized user");

            try
            {
                var mensage = await mediator.Send(new AccountStatusRequest
                {
                    AccountNumber = authorizationResult.Value,
                    AccountStatus = AccountStatus.Active
                });

                return Ok(mensage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("inactive-account")]
        public async Task<IActionResult> InactiveAsync()
        {
            var authorizationResult = CheckAccountClaim();
            if (authorizationResult == null)
                return Unauthorized("Unauthorized user");

            try
            {
                var mensage = await mediator.Send(new AccountStatusRequest
                {
                    AccountNumber = authorizationResult.Value,
                    AccountStatus = AccountStatus.Inactive
                });

                return Ok(mensage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("extract-account")]
        public async Task<ActionResult<PaginationResponse<ClientExtractViewModel>>> GetListExtract([FromQuery] FilterClientDto filterClient)
        {
            var authorizationResult = CheckAccountClaim();
            if (authorizationResult == null)
                return Unauthorized("Unauthorized user");


            var filter = new FilterClient
            {
                InitialDate = filterClient.InitialDate,
                FinalDate = filterClient.FinalDate,
                NumberAccount = authorizationResult.Value,
                NumberAgency = 1,
                FlowType = filterClient.FlowType,
                _page = filterClient._page,
                _size = filterClient._size,
                _order = filterClient._order
            };


            if (authorizationResult == null)
            {
                return Unauthorized("Unauthorized user");
            }

            try
            {
                return await mediator.Send(new ListExtractClientQuery
                {
                    FilterClient = filter
                });
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
