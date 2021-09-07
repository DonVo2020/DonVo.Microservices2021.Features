using Polly.CircuitBreaker;
using Polly.Registry;
using System;
using System.Collections.Generic;

namespace PollyManagement.CircuitBreakers
{
    public class CircuitBreakerManager : ICircuitBreakerManager
    {
        public readonly IPolicyRegistry<string> Registry;
        private readonly object _lock = new();

        public CircuitBreakerManager()
        {
            Registry = new PolicyRegistry();
        }

        public bool TryAdd<TPolicy>(string key, TPolicy policy) where TPolicy : ICircuitBreakerPolicy
        {
            var added = false;
            lock (_lock)
            {
                if (!Registry.ContainsKey(key))
                {
                    Registry.Add(key, policy);
                    added = true;
                }
            }
            return added;
        }

        public bool TryGet<TPolicy>(string key, out TPolicy policy) where TPolicy : ICircuitBreakerPolicy
        {
            return Registry.TryGet(key, out policy);
        }

        public TPolicy GetOrAdd<TPolicy>(string key, TPolicy policy) where TPolicy : ICircuitBreakerPolicy
        {
            //Polly 7.2.2 does implement GetOrAdd
            //return Registry.GetOrAdd(key, policy);
            TPolicy registeredPolicy = default;
            lock (_lock)
            {
                if (!Registry.TryGet(key, out registeredPolicy))
                {
                    Registry.Add(key, policy);
                    registeredPolicy = policy;
                }
            }
            return registeredPolicy;
        }

        public IEnumerable<string> GetKeys()
        {
            foreach (var policy in Registry)
            {
                yield return policy.Key;
            }
        }

        public IDictionary<string, CircuitState> GetCircuitStates()
        {
            var dict = new Dictionary<string, CircuitState>(Registry.Count);

            foreach (var policy in Registry)
            {
                dict.Add(policy.Key, Registry.Get<ICircuitBreakerPolicy>(policy.Key).CircuitState);
            }

            return dict;
        }

        public CircuitState GetCircuitState(string key)
        {
            ThrowOnNotRegistered(key, out ICircuitBreakerPolicy policy);

            return policy.CircuitState;
        }

        public Exception GetLastException(string key)
        {
            ThrowOnNotRegistered(key, out ICircuitBreakerPolicy policy);

            return policy.LastException;
        }

        public bool TryIsolate(string key)
        {
            ThrowOnNotRegistered(key, out ICircuitBreakerPolicy policy);

            policy.Isolate();
            return policy.CircuitState == CircuitState.Isolated;
        }

        public bool TryReset(string key)
        {
            ThrowOnNotRegistered(key, out ICircuitBreakerPolicy policy);

            policy.Reset();
            return policy.CircuitState == CircuitState.Closed;
        }

        private void ThrowOnNotRegistered(string key, out ICircuitBreakerPolicy policy)
        {
            if (!Registry.TryGet(key, out policy))
            {
                throw new ArgumentException($"No circuitbreaker registered with this key {key}", nameof(key));
            }
        }
    }
}
