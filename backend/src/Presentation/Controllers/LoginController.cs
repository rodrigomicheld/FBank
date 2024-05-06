using Application.Requests.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
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
                    Document = RemoveDocumentMask(tokenRequest.Document),
                    Password = tokenRequest.Password
                });

                return Ok("{\"token\": \""+token+"\"}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string RemoveDocumentMask(string document)
        {
            return document.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
        }
    }
}