using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PollyManagement.CircuitBreakers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementController : ControllerBase
    {
        private readonly ILogger<ManagementController> _logger;
        private readonly ICircuitBreakerManager _circuitBreakerManager;

        public ManagementController(ILogger<ManagementController> logger, ICircuitBreakerManager circuitBreakerManager)
        {
            _logger = logger;
            _circuitBreakerManager = circuitBreakerManager;
        }

        //This shows the circuitbreakers that are registered with the `CircuitBreakerManager`.
        //The `Ideal-Policy` will always be present, since it is registered at startup.
        //The `BetterController-Policy` will only be present once `/api/better/willbreak` has been requested.
        //The policy used in `WrongController` won't ever be shown (obviously).
        [HttpGet("list")]
        public ActionResult List()
        {
            _logger.LogInformation("circuitBreakerManager.GetCircuitStates()");
            return Ok(_circuitBreakerManager.GetCircuitStates());
        }
    }
}
