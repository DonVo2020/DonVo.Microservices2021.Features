using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.CircuitBreaker;
using System;
using System.Threading.Tasks;

namespace PollyManagement.CircuitBreakers.API.Controllers
{
    // Controllers are added to the DI container by using `AddControllersAsServices` in the `Startup`
    // This will declare them as Transient: https://github.com/dotnet/aspnetcore/blob/c7cb8467bfce721e2d66ef3862cd8c7c1fdbb421/src/Mvc/Mvc.Core/src/DependencyInjection/MvcCoreMvcBuilderExtensions.cs#L167
    // Meaning, they are created each time they're requested from the service container.
    // Therefore with every request the `_policy` is a new one and it won't break the circuit (even after refreshing serveral times).
    // In this case you can break the circuit with `WillBreak` but `WontBreak` won't be affected.
    [Route("api/[controller]")]
    [ApiController]
    public class WrongController : ControllerBase
    {
        private readonly ILogger<WrongController> _logger;
        private readonly AsyncCircuitBreakerPolicy _policy;

        public WrongController(ILogger<WrongController> logger)
        {
            _logger = logger;

            _policy = Policy.Handle<Exception>()
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
        }

        //This won't break the circuit because with every request a new policy is executed
        [HttpGet("wontbreak")]
        public async Task<ActionResult> WontBreak()
        {
            await _policy.ExecuteAndCaptureAsync(() => throw new Exception("Oh sh*t!"));

            return Ok();
        }

        //This will break the circuit because the same policy is executed several times
        [HttpGet("willbreak")]
        public async Task<ActionResult> WillBreak()
        {
            for (var i = 0; i < 10; i++)
            {
                await _policy.ExecuteAndCaptureAsync(() => throw new Exception("Oh sh*t!"));
            }

            return Ok();
        }
    }
}
