using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Polly.CircuitBreaker;
using System.Collections.Generic;

namespace PollyManagement.CircuitBreakers
{
    public static class CircuitBreakerManagerExtensions
    {
        public static IServiceCollection AddPollyRegistry(this IServiceCollection services)
        {
            services.TryAddSingleton<ICircuitBreakerManager>(new CircuitBreakerManager());
            return services;
        }

        public static IServiceCollection AddPollyPolicy<TPolicy>(this IServiceCollection services, string policyName, TPolicy policy) where TPolicy : ICircuitBreakerPolicy
        {
            _ = GetOrAddManager(services).TryAdd(policyName, policy);
            return services;
        }

        public static IServiceCollection AddPollyPolicies<TPolicy>(this IServiceCollection services, IDictionary<string, TPolicy> policies) where TPolicy : ICircuitBreakerPolicy
        {
            var manager = GetOrAddManager(services);
            foreach (var policy in policies)
            {
                _ = manager.TryAdd(policy.Key, policy.Value);
            }

            return services;
        }

        private static ICircuitBreakerManager GetOrAddManager(IServiceCollection services)
        {
            ICircuitBreakerManager manager;

            using (var provider = services.BuildServiceProvider())
            {
                manager = provider.GetService<ICircuitBreakerManager>();
                if (manager == null)
                {
                    manager = new CircuitBreakerManager();
                    _ = services.AddSingleton(manager);
                }
            }

            return manager;
        }
    }
}
