using FBank.Application.Queries.Accounts;
using FBank.Application.Requests.Accounts;
using FBank.Application.ViewMoldels;
using FBank.Domain.Common;
using FBank.Domain.Common.Filters;
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

        [HttpGet("client")]
        public async Task<ActionResult<ClientViewModel>> GetOneAsync([FromQuery] string document)
        {
            var authorizationResult = CheckDocumentClaim(document);
            if (authorizationResult != null)
                return authorizationResult;

            try
            {
                return await mediator.Send(new GetOneClientQuery { Document = RemoveDocumentMask(document) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("client")]
        public async Task<IActionResult> RegisterAsync([FromBody] PostOneClientRequest client)
        {
            try
            {
                var account = await mediator.Send(new PostOneClientRequest
                {
                    Document = RemoveDocumentMask(client.Document),
                    Name = client.Name,
                    Password = client.Password
                });

                return Ok($"Account successfully registered. {account}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("active")]
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

        [HttpPut("inactive")]
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

        [HttpGet("extract")]
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

        private ActionResult CheckDocumentClaim(string document)
        {
            var user = HttpContext.User;

            if (!user.Identity.IsAuthenticated)
                return Unauthorized("Unauthorized user");

            var documentClaim = user.Claims.FirstOrDefault(c => c.Type == "Document");

            if (documentClaim == null || documentClaim.Value != RemoveDocumentMask(document))
                return Unauthorized("User does not have permission.");

            return null;
        }

        private string RemoveDocumentMask(string document)
        {
            return document.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
        }
    }
}
