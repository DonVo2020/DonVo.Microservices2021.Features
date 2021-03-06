using DonVo.Throttled.API;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace DonVo.Throttled.xUnitTest
{
    public class ValuesControllerTests
    {
        [Theory]
        [InlineData(1, "false")]
        [InlineData(2, "true")]
        [InlineData(11, "true")]
        [InlineData(12, "false")]
        public async Task TestWithDataSource(int n, string isPrime)
        {
            using var factory = new WebApplicationFactory<Startup>();
            var httpClient = factory.CreateClient();
            var response = await httpClient.GetAsync($"api/values/isPrime?number={n}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            Assert.Equal(isPrime, result);
        }

        [Fact]
        public async Task ExpectExceptionWhenExceedRateLimit()
        {
            using var factory = new WebApplicationFactory<Startup>();
            var httpClient = factory.CreateClient();
            var numbers = new List<long> { 1, 12, 11 };
            var allTasks = numbers.Select(n => Task.Run(async () =>
            {
                var result = await httpClient.GetStringAsync($"api/values/isPrime?number={n}");
                Console.WriteLine($"{n} is a prime number? {result}");
            })).ToList();
            async Task ConcurrentApiRequests() => await Task.WhenAll(allTasks);
            var e = await Assert.ThrowsAsync<HttpRequestException>(ConcurrentApiRequests);
            Assert.Equal("Response status code does not indicate success: 429 (Too Many Requests).", e.Message);
        }

        [Fact]
        public async Task ExpectHttpHeadersWhenExceedRateLimit()
        {
            using var factory = new WebApplicationFactory<Startup>();
            var httpClient = factory.CreateClient();
            httpClient.DefaultRequestHeaders.Clear();
            var numbers = new List<long> { 1, 12, 11 };
            var allTasks = numbers.Select(n => Task.Run(async () =>
            {
                var response = await httpClient.GetAsync($"api/values/isPrime?number={n}");
                return new
                {
                    Number = n,
                    Headers = response.Headers.ToList()
                };
            })).ToList();

            var results = await Task.WhenAll(allTasks);

            // assert
            var retryHeaders = results.SelectMany(x => x.Headers).Where(x => x.Key == "Retry-After").ToList();
            Assert.Single(retryHeaders);
            Assert.Equal("1", string.Join(", ", retryHeaders[0].Value));
            var xRateLimitHeaders = results.SelectMany(x => x.Headers).Where(x => x.Key.StartsWith("X-Rate-Limit")).ToList();
            Assert.Equal(6, xRateLimitHeaders.Count);

            // auxiliary method to print out all headers.
            foreach (var result in results)
            {
                Console.WriteLine($"\r\nHTTP Response Headers for number = {result.Number}:");
                foreach (var (key, value) in result.Headers)
                {
                    Console.WriteLine($"\t{key}: {string.Join(", ", value)}");
                }
            }
        }
        /* Test Output

        TEST Server Started.

        HTTP Response Headers for number = 1:
            Retry-After: 1

        HTTP Response Headers for number = 12:
            X-Rate-Limit-Limit: 1s
            X-Rate-Limit-Remaining: 1
            X-Rate-Limit-Reset: 2019-09-17T18:51:02.4401731Z

        HTTP Response Headers for number = 11:
            X-Rate-Limit-Limit: 1s
            X-Rate-Limit-Remaining: 0
            X-Rate-Limit-Reset: 2019-09-17T18:51:02.4401731Z

        */

        [Fact]
        public async Task TestWithSemaphoreSlim()
        {
            using var factory = new WebApplicationFactory<Startup>();
            var httpClient = factory.CreateClient();
            var numbers = new List<long> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 0, 11, 13, 17, 19, 23, 29, 31, 41, 43, 1763, 7400854980481283 };

            var throttler = new System.Threading.SemaphoreSlim(2);
            var tasks = numbers.Select(async n =>
            {
                await throttler.WaitAsync();

                var task = httpClient.GetStringAsync($"api/values/isPrime?number={n}");
                _ = task.ContinueWith(async _ =>
                {
                    await Task.Delay(1000);
                    throttler.Release();
                });
                try
                {
                    var isPrime = await task;
                    return new
                    {
                        Number = n,
                        IsPrime = isPrime
                    };
                }
                catch (HttpRequestException)
                {
                    return new
                    {
                        Number = n,
                        IsPrime = "NA"
                    };
                }
            });
            var results = await Task.WhenAll(tasks);

            // assert
            var expectedPrimeNumbers = new List<long> { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 41, 43 };
            var expectedNonPrimeNumbers = new List<long> { 1, 4, 6, 8, 9, 10, 1763, 7400854980481283 };
            Assert.Equal(expectedPrimeNumbers, results.Where(x => x.IsPrime == "true").Select(x => x.Number).ToList());
            Assert.Equal(expectedNonPrimeNumbers, results.Where(x => x.IsPrime == "false").Select(x => x.Number).ToList());
            Assert.Equal("NA", results.First(x => x.Number == 0).IsPrime);
        }
    }
}
