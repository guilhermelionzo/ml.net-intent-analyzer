using IntentAnalysis.Domain.Handler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IntentAnalysis.Api.Controllers
{
    [ApiController]
    [Route("v1/intent")]
    public class IntentController : ControllerBase
    {

        private readonly ILogger<IntentController> _logger;

        public IntentController(ILogger<IntentController> logger)
        {
            _logger = logger;
        }

        [HttpPost("/{intent}")]
        public string GetIntent(
            [FromRoute] string intent,
            [FromServices] IntentHandler handler
        )
        {   
            return handler.ReturnIntent(intent);
        }
    }
}
