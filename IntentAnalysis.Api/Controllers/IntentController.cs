using IntentAnalysis.Domain.Commands;
using IntentAnalysis.Domain.Handler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IntentAnalysis.Api.Controllers
{
    [ApiController]
    [Route("v1/intent")]
    public class IntentController : ControllerBase
    {
        public CommandResult GetIntent(
            [FromRoute] string intent,
            [FromServices] IntentHandler handler
        )
        {
            CommandResult result = null;
            try
            {
                result = new CommandResult(true, "Intention was predicted.", handler.ReturnIntent(intent));
            }
            catch (System.Exception e)
            {
                result = new CommandResult(true, "An error has occurred.", e.Data);
            }

            return result;
        }
    }
}
