using FBank.Application.Requests.Token;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FBank.Presentation.Controllers
{
    public class LoginController : StandardController
    {
        public LoginController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<ActionResult<string>> Authentication([FromQuery] TokenRequest tokenRequest)
        {
            try
            {
                var token =  await mediator.Send(new TokenRequest
                {
                    NumberAgency = tokenRequest.NumberAgency,
                    NumberAccount = tokenRequest.NumberAccount,
                    Password = tokenRequest.Password
                });

                return Ok("{\"token\": \""+token+"\"}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}