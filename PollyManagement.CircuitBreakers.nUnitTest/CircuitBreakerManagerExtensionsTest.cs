using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Polly;
using Polly.CircuitBreaker;
using System;
using System.Collections.Generic;
using System.Linq;
using ServiceCollectionImpl = Microsoft.Extensions.DependencyInjection.ServiceCollection;

namespace PollyManagement.CircuitBreakers.nUnitTest
{
    internal class CircuitBreakerManagerExtensionTest
    {
        [Test]
        public void AddPollyRegistry_ShouldRegisterCircuitBreakerManagerAsSingleton()
        {
            var serviceCollection = new ServiceCollectionImpl();

            serviceCollection.AddPollyRegistry();

            AssertManagerRegistered(serviceCollection);
        }

        [Test]
        public void AddPollyRegistry_IfManagerHasBeenRegistered_ThenShouldNotReregister()
        {
            const string registeredPolicyName = "Test";
            var serviceCollection = SetupRegisteredManager(registeredPolicyName);

            serviceCollection.AddPollyRegistry();

            Assert.AreEqual(1, serviceCollection.Count);
            var instance = serviceCollection[0].ImplementationInstance as CircuitBreakerManager;
            AssertManagerContainsPolicy(instance, registeredPolicyName);
        }

        [Test]
        public void AddPollyPolicy_ShouldRegisterCircuitBreakerManagerAsSingleton()
        {
            var serviceCollection = new ServiceCollectionImpl();

            serviceCollection.AddPollyPolicy("Test", GetCircuitBreakerPolicy());

            AssertManagerRegistered(serviceCollection);
        }

        [Test]
        public void AddPollyPolicy_ShouldAddPolicyToRegisteredManager()
        {
            var serviceCollection = SetupRegisteredManager();
            const string registeredPolicyName = "Test";

            serviceCollection.AddPollyPolicy(registeredPolicyName, GetCircuitBreakerPolicy());

            var instance = serviceCollection[0].ImplementationInstance as CircuitBreakerManager;
            AssertManagerContainsPolicy(instance, registeredPolicyName);
        }

        [Test]
        public void AddPollyPolicies_ShouldRegisterCircuitBreakerManagerAsSingleton()
        {
            var serviceCollection = new ServiceCollectionImpl();
            var dict = new Dictionary<string, ICircuitBreakerPolicy> { { "Test", GetCircuitBreakerPolicy() } };

            serviceCollection.AddPollyPolicies(dict);

            AssertManagerRegistered(serviceCollection);
        }

        [Test]
        public void AddPollyPolicies_ShouldAddAllPoliciesToRegisteredManager()
        {
            var serviceCollection = SetupRegisteredManager();
            var dict = new Dictionary<string, ICircuitBreakerPolicy> {
                { "Test", GetCircuitBreakerPolicy() },
                { "Test 2", GetCircuitBreakerPolicy() }
            };

            serviceCollection.AddPollyPolicies(dict);

            var instance = serviceCollection[0].ImplementationInstance as CircuitBreakerManager;
            AssertManagerContainsPolicies(instance, dict);
        }

        private static IServiceCollection SetupRegisteredManager(string policyName = null)
        {
            var serviceCollection = new ServiceCollectionImpl();
            var manager = new CircuitBreakerManager();
            if (!string.IsNullOrEmpty(policyName))
            {
                manager.TryAdd(policyName, GetCircuitBreakerPolicy());
            }
            serviceCollection.AddSingleton<ICircuitBreakerManager>(manager);

            return serviceCollection;
        }

        private static void AssertManagerRegistered(IServiceCollection services)
        {
            Assert.AreEqual(1, services.Count);
            var registered = services[0];
            Assert.AreEqual(ServiceLifetime.Singleton, registered.Lifetime);
            Assert.IsNotNull(registered.ImplementationInstance);
            Assert.AreEqual(typeof(ICircuitBreakerManager), registered.ServiceType);
            Assert.IsInstanceOf<CircuitBreakerManager>(registered.ImplementationInstance);
        }

        private static void AssertManagerContainsPolicy(CircuitBreakerManager instance, string policyName)
        {
            Assert.AreEqual(1, instance.GetKeys().Count());
            Assert.AreEqual(policyName, instance.GetKeys().ElementAt(0));
        }

        private static void AssertManagerContainsPolicies(CircuitBreakerManager instance, IDictionary<string, ICircuitBreakerPolicy> policies)
        {
            var keys = instance.GetKeys();
            Assert.AreEqual(policies.Count, keys.Count());
            foreach (var policy in policies)
            {
                Assert.IsTrue(keys.Contains(policy.Key));
            }
        }

        private static CircuitBreakerPolicy GetCircuitBreakerPolicy()
        {
            return Policy.Handle<Exception>().AdvancedCircuitBreaker(1.0, TimeSpan.FromSeconds(1), 2, TimeSpan.FromMinutes(1));
        }
    }
}