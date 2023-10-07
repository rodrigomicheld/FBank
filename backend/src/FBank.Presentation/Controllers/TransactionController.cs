using Microsoft.AspNetCore.Mvc;

namespace FBank.Presentation.Controllers
{
    public class TransactionController : ControllerBase
    {
        private readonly ILogger _logger;
        public TransactionController(Logger<TransactionController> logger)
        {
            _logger = logger;
        }


    }
}
