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
            var authorizationResult = CheckDocumentClaim(document);
            if (authorizationResult != null)
            {
                return authorizationResult;
            }

            try
            {
                return await mediator.Send(new GetOneClientQuery { Document = RemoveDocumentMask(document) });
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
                var account = await mediator.Send(new PostOneClientQuery 
                { 
                    Document = RemoveDocumentMask(client.Document),
                    Name = client.Name,
                    Password = client.Password
                });

                return Ok($"Account successfully registered. {account}" );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private ActionResult CheckDocumentClaim(string document)
        {
            var user = HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                return Unauthorized("Unauthorized user");
            }

            var documentClaim = user.Claims.FirstOrDefault(c => c.Type == "Document");

            if (documentClaim == null || documentClaim.Value != RemoveDocumentMask(document))
            {
                return Unauthorized("User does not have permission.");
            }

            return null;
        }

        private string RemoveDocumentMask(string document)
        {
            return document.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
        }
    }
}
