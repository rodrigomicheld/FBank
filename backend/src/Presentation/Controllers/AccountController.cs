﻿using Application.Queries.Accounts;
using Application.Requests.Accounts;
using Application.ViewMoldels;
using Domain.Common;
using Domain.Common.Filters;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class AccountController : StandardController
    {
        public AccountController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("client")]
        public async Task<ActionResult<ClientViewModel>> GetOneAsync()
        {
            var authorizationResult = CheckDocumentClaim();
            if (authorizationResult == null)
                return Unauthorized("Unauthorized user");

            try
            {
                return await mediator.Send(new GetOneClientQuery { Document = authorizationResult });
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
                InitialDate = new DateTime(filterClient.InitialDate.Year, filterClient.InitialDate.Month, filterClient.InitialDate.Day, 00, 00, 00, 000),
                FinalDate = new DateTime(filterClient.FinalDate.Year, filterClient.FinalDate.Month, filterClient.FinalDate.Day, 23, 59, 59, 999),
                NumberAccount = authorizationResult.Value,
                NumberAgency = 1,
                FlowType = filterClient.FlowType,
                _page = filterClient._page,
                _size = filterClient._size,
                _order = filterClient._order
            };

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

            var accountClaim = user.Claims.FirstOrDefault(c => c.Type == "Account");

            if (accountClaim == null)
                return null;

            if (int.TryParse(accountClaim.Value, out int accountValue))
                return accountValue;

            return null;
        }

        private string CheckDocumentClaim()
        {
            var user = HttpContext.User;

            if (!user.Identity.IsAuthenticated)
                return null;

            var documentClaim = user.Claims.FirstOrDefault(c => c.Type == "Document");

            if (documentClaim == null)
                return null;

            return documentClaim.Value;
        }
    }
}
