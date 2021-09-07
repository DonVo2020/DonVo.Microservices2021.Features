using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polly.CircuitBreaker;
using System;
using System.Threading.Tasks;

namespace PollyManagement.CircuitBreakers.API.Controllers
{
    //This controller is the ideal situation: the policy is defined elsewhere (in this example the `Startup` has been used)
    //so we can be sure the policy is created only once, and will be re-used.
    //Furthermore, because the policy is defined in a central place, it can be managed (i.e. opened or closed) from another controller if the need arises.
    [Route("api/[controller]")]
    [ApiController]
    public class IdealController : ControllerBase
    {
        private readonly ILogger<IdealController> _logger;
        private readonly ICircuitBreakerManager _circuitBreakerManager;

        public IdealController(ILogger<IdealController> logger, ICircuitBreakerManager circuitBreakerManager)
        {
            _logger = logger;
            _circuitBreakerManager = circuitBreakerManager;
        }

        //This will break the circuit because the same policy is executed several times.
        //The policy is defined outside the controller
        [HttpGet("willbreak")]
        public async Task<ActionResult> WillBreak()
        {
            _circuitBreakerManager.TryGet("Ideal-Policy", out AsyncCircuitBreakerPolicy policy);

            await policy.ExecuteAndCaptureAsync(() =>
                {
                    _logger.LogError("Policy Will Break !!!");
                    throw new Exception("Oh sh*t!");
                }
            );

            return Ok();
        }
    }
}
