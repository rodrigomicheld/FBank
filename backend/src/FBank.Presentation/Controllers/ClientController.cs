using FBank.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FBank.Presentation.Controllers
{
    [ApiController]
    [Route("Client")]
    public class ClientController : ControllerBase
    {
        private IClientRepository _clientRepository;
        private readonly ILogger<ClientController> _logger;
        public ClientController(
            IClientRepository clientRepository,
            ILogger<ClientController> logger)
        {
            _clientRepository = clientRepository;
            _logger = logger;
        }

        [HttpGet("obter-client-por-id/{id}")]
        public IActionResult FindClientToId(Guid id)
        {
            _logger.LogInformation("Executando o metodo FindClientToId");
            return Ok(_clientRepository.SelectToId(id));
        }
    }
}
