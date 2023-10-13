using FBank.Application.Queries;
using FBank.Application.ViewMoldels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FBank.Presentation.Controllers
{
    public class ClientController : StandardController
    {
        public ClientController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<ActionResult<ClientViewModel>> GetOneAsync([FromQuery] string document)
        {
            try
            {
                return await mediator.Send(new GetOneClientQuery { Document = document.Trim().Replace(".", "").Replace("-", "").Replace("/", "") });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register-account")]
        public async Task<IActionResult> RegisterAsync([FromBody] PostOneClientQuery client)
        {
            try
            {
                var conta = await mediator.Send(new PostOneClientQuery 
                { 
                    Document = client.Document.Trim().Replace(".", "").Replace("-", "").Replace("/", ""),
                    Name = client.Name
                });

                return Ok($"Account successfully registered. Number: {conta}" );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
