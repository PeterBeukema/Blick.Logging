using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blick.Logging.TestApi.Controllers;

[ApiController, Route("logger")]
public class LoggerController : ControllerBase
{
    private readonly ILogger<LoggerController> logger;

    public LoggerController(ILogger<LoggerController> logger)
    {
        this.logger = logger;
    }

    [HttpPost(Name = "TriggerLogging")]
    public ActionResult TriggerLogging()
    {
        logger.LogTrace("This is a test log (trace)");
        logger.LogDebug("This is a test log (debug)");
        logger.LogInformation("This is a test log (information)");
        logger.LogWarning("This is a test log (warning)");
        logger.LogError("This is a test log (error)");
        logger.LogCritical("This is a test log (critical)");

        return Ok();
    }
}