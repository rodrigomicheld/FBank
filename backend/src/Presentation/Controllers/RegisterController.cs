using Application.Requests.Accounts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class RegisterController : StandardController
    {
        public RegisterController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
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

        private string RemoveDocumentMask(string document)
        {
            return document.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
        }
    }
}
