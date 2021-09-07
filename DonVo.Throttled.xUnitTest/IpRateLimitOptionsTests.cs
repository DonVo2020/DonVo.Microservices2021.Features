using AspNetCoreRateLimit;
using DonVo.Throttled.API;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace DonVo.Throttled.xUnitTest
{
    public class IpRateLimitOptionsTests
    {
        [Fact]
        public void CheckIpRateLimitOptions()
        {
            using var factory = new WebApplicationFactory<Startup>();
            var options = factory.Services.GetRequiredService<IOptions<IpRateLimitOptions>>();
            Assert.Single(options.Value.GeneralRules);
            var generalRule = options.Value.GeneralRules[0];
            Assert.Equal("*:/api/*", generalRule.Endpoint);
            Assert.Equal("1s", generalRule.Period);
            Assert.Equal(2, generalRule.Limit);
        }
    }
}
