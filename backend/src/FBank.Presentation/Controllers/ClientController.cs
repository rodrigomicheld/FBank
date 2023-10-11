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
                return await mediator.Send(new GetOneClientQuery { Document = document });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
