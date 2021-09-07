using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.CircuitBreaker;
using System;
using System.Threading.Tasks;

namespace PollyManagement.CircuitBreakers.API.Controllers
{
    //This controller is better than the `WrongController` in the sense that it uses the same policy with all requests.
    //However, since it is Transient, with every request a `policy` is created and the CircuitBreakerManager is checked.
    [Route("api/[controller]")]
    [ApiController]
    public class BetterController : ControllerBase
    {
        private readonly ILogger<BetterController> _logger;
        private readonly ICircuitBreakerManager _circuitBreakerManager;

        private const string PolicyName = "BetterController-Policy";

        public BetterController(ILogger<BetterController> logger, ICircuitBreakerManager circuitBreakerManager)
        {
            _logger = logger;
            _circuitBreakerManager = circuitBreakerManager;

            var policy = Policy.Handle<Exception>()
                .AdvancedCircuitBreakerAsync(failureThreshold: 0.5,
                    samplingDuration: TimeSpan.FromMinutes(2),
                    minimumThroughput: 2,
                    durationOfBreak: TimeSpan.FromMinutes(5),
                    onBreak: (Exception e, TimeSpan span) =>
                    {
                        _logger.LogError("Policy did break");
                    },
                    onReset: () =>
                    {
                        _logger.LogInformation("Policy did reset");
                    });

            var isAdded = _circuitBreakerManager.TryAdd(PolicyName, policy); //True on the first request, False on subsequent requests
            _logger.LogInformation($"Has the policy been added? {isAdded}");
        }

        //This will break the circuit because the same policy will be executed.
        //The policy is stored in a Singleton manager so `TryGet` will return the same policy for all calls.
        [HttpGet("willbreak")]
        public async Task<ActionResult> WillBreak()
        {
            _circuitBreakerManager.TryGet(PolicyName, out AsyncCircuitBreakerPolicy policy);

            await policy.ExecuteAndCaptureAsync(() => { _logger.LogInformation("Policy did break"); throw new Exception("Oh sh*t!"); });

            return Ok("Policy did break");
        }
    }
}
