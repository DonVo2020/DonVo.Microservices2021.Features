using Polly.CircuitBreaker;
using System;
using System.Collections.Generic;

namespace PollyManagement.CircuitBreakers
{
    public interface ICircuitBreakerManager
    {
        bool TryAdd<TPolicy>(string key, TPolicy policy) where TPolicy : ICircuitBreakerPolicy;
        bool TryGet<TPolicy>(string key, out TPolicy policy) where TPolicy : ICircuitBreakerPolicy;
        TPolicy GetOrAdd<TPolicy>(string key, TPolicy policy) where TPolicy : ICircuitBreakerPolicy;
        IEnumerable<string> GetKeys();
        IDictionary<string, CircuitState> GetCircuitStates();
        CircuitState GetCircuitState(string key);
        Exception GetLastException(string key);
        bool TryIsolate(string key);
        bool TryReset(string key);
    }
}
