using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/fbank/[controller]")]
    public abstract class StandardController : ControllerBase
    {
        protected IMediator mediator;
        protected StandardController(IMediator mediator) 
        {
            this.mediator = mediator;
        }
    }
}
