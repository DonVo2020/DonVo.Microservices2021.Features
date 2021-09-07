using NUnit.Framework;
using Polly;
using Polly.CircuitBreaker;
using System;
using System.Linq;

namespace PollyManagement.CircuitBreakers.nUnitTest
{
    [TestFixture]
    internal class CircuitBreakerManagerDefaultPolicyTest : AbstractCircuitBreakerManagerTest<CircuitBreakerPolicy>
    {
        protected override CircuitBreakerPolicy GetCircuitBreakerPolicy()
        {
            return Policy.Handle<Exception>().AdvancedCircuitBreaker(1.0, TimeSpan.FromSeconds(1), 2, TimeSpan.FromMinutes(1));
        }
    }

    [TestFixture]
    internal class CircuitBreakerManagerDefaultTResultPolicyTest : AbstractCircuitBreakerManagerTest<CircuitBreakerPolicy<object>>
    {
        protected override CircuitBreakerPolicy<object> GetCircuitBreakerPolicy()
        {
            return Policy<object>.Handle<Exception>().AdvancedCircuitBreaker(1.0, TimeSpan.FromSeconds(1), 2, TimeSpan.FromMinutes(1));
        }
    }

    [TestFixture]
    internal class CircuitBreakerManagerAsyncPolicyTest : AbstractCircuitBreakerManagerTest<AsyncCircuitBreakerPolicy>
    {
        protected override AsyncCircuitBreakerPolicy GetCircuitBreakerPolicy()
        {
            return Policy.Handle<Exception>().AdvancedCircuitBreakerAsync(1.0, TimeSpan.FromSeconds(1), 2, TimeSpan.FromMinutes(1));
        }
    }

    [TestFixture]
    internal class CircuitBreakerManagerAsyncTResultPolicyTest : AbstractCircuitBreakerManagerTest<AsyncCircuitBreakerPolicy<object>>
    {
        protected override AsyncCircuitBreakerPolicy<object> GetCircuitBreakerPolicy()
        {
            return Policy<object>.Handle<Exception>().AdvancedCircuitBreakerAsync(1.0, TimeSpan.FromSeconds(1), 2, TimeSpan.FromMinutes(1));
        }
    }

    internal abstract class AbstractCircuitBreakerManagerTest<TPolicy> where TPolicy : ICircuitBreakerPolicy
    {
        private CircuitBreakerManager _sut;
        private TPolicy _testPolicy;
        private const string TestKey = "test";

        protected abstract TPolicy GetCircuitBreakerPolicy();

        [SetUp]
        public void Setup()
        {
            _sut = new CircuitBreakerManager();
            _sut.Registry.Clear();
            _testPolicy = GetCircuitBreakerPolicy();
        }

        [Test]
        public void TryAdd_IfPolicyDoesNotExist_ThenAddPolicyAndReturnTrue()
        {
            var result = _sut.TryAdd(TestKey, _testPolicy);

            Assert.AreEqual(1, _sut.Registry.Count);
            Assert.IsTrue(_sut.Registry.ContainsKey(TestKey));
            Assert.IsTrue(result);
        }

        [Test]
        public void TryAdd_IfPolicyDoesExist_ThenReturnFalse()
        {
            _sut.Registry.Add(TestKey, GetCircuitBreakerPolicy());

            var result = _sut.TryAdd(TestKey, _testPolicy);

            Assert.AreEqual(1, _sut.Registry.Count);
            Assert.IsTrue(_sut.Registry.ContainsKey(TestKey));
            Assert.IsFalse(result);
        }

        [Test]
        public void TryAdd_IfPolicyWithDifferentKeyCasing_ThenAddPolicyAndReturnTrue()
        {
            var initialPolicy = GetCircuitBreakerPolicy();
            _sut.Registry.Add(TestKey, initialPolicy);
            var result = _sut.TryAdd(TestKey.ToUpper(), _testPolicy);

            Assert.AreEqual(2, _sut.Registry.Count);
            Assert.IsTrue(_sut.Registry.ContainsKey(TestKey));
            Assert.IsTrue(result);
        }

        [Test]
        public void TryGet_IfPolicyDoesNotExist_ThenReturnFalseAndNullOutParam()
        {
            var result = _sut.TryGet(TestKey, out ICircuitBreakerPolicy policy);

            Assert.IsFalse(result);
            Assert.IsNull(policy);
        }

        [Test]
        public void TryGet_IfPolicyDoesExist_ThenReturnTrueAndNotNullOutParam()
        {
            _sut.Registry.Add(TestKey, _testPolicy);

            var result = _sut.TryGet(TestKey, out ICircuitBreakerPolicy policy);

            Assert.IsTrue(result);
            Assert.IsNotNull(policy);
            Assert.AreSame(_testPolicy, policy);
        }

        [Test]
        public void GetOrAdd_IfPolicyDoesNotExist_ThenReturnAddedPolicy()
        {
            var policy = _sut.GetOrAdd(TestKey, _testPolicy);

            Assert.AreEqual(1, _sut.Registry.Count);
            Assert.IsTrue(_sut.Registry.ContainsKey(TestKey));
            Assert.AreSame(_testPolicy, policy);
        }

        [Test]
        public void GetOrAdd_IfPolicyWithDifferentKeyCasing_ThenReturnAddedPolicy()
        {
            var initialPolicy = GetCircuitBreakerPolicy();
            _sut.Registry.Add(TestKey, initialPolicy);
            var policy = _sut.GetOrAdd(TestKey.ToUpper(), _testPolicy);

            Assert.AreEqual(2, _sut.Registry.Count);
            Assert.IsTrue(_sut.Registry.ContainsKey(TestKey));
            Assert.AreSame(_testPolicy, policy);
            Assert.AreNotSame(initialPolicy, policy);
        }

        [Test]
        public void GetOrAdd_IfPolicyDoesExist_ThenReturnExistingPolicy()
        {
            _sut.Registry.Add(TestKey, GetCircuitBreakerPolicy());

            var policy = _sut.GetOrAdd(TestKey, _testPolicy);

            Assert.AreEqual(1, _sut.Registry.Count);
            Assert.IsTrue(_sut.Registry.ContainsKey(TestKey));
            Assert.AreNotSame(_testPolicy, policy);
        }

        [Test]
        public void GetKeys_IfDoesNotContainPolicies_ThenReturnEmptyResult()
        {
            var keys = _sut.GetKeys();

            Assert.IsEmpty(keys);
        }

        [Test]
        public void GetKeys_IfDoesContainPolicies_ThenReturnAllKeys()
        {
            _sut.Registry.Add("a", GetCircuitBreakerPolicy());
            _sut.Registry.Add("b", GetCircuitBreakerPolicy());

            var keys = _sut.GetKeys();

            Assert.IsNotEmpty(keys);
            Assert.IsTrue(keys.Any(key => key == "a"));
            Assert.IsTrue(keys.Any(key => key == "b"));
            Assert.IsFalse(keys.Any(key => key == "c"));
        }

        [Test]
        public void GetCircuitStates_IfDoesNotContainPolicies_ThenReturnEmptyResult()
        {
            var states = _sut.GetCircuitStates();

            Assert.IsEmpty(states.Keys);
        }

        [Test]
        public void GetCircuitStates_IfDoesContainPolicies_ThenReturnAllStates()
        {
            var policyA = GetCircuitBreakerPolicy();
            var policyB = GetCircuitBreakerPolicy();
            _sut.Registry.Add("a", policyA);
            _sut.Registry.Add("b", policyB);

            var states = _sut.GetCircuitStates();

            Assert.IsNotEmpty(states.Keys);
            Assert.AreEqual(policyA.CircuitState, states["a"]);
            Assert.AreEqual(policyB.CircuitState, states["b"]);
        }

        [Test]
        public void GetCircuitState_IfKeyDoesNotExist_ThenThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _sut.GetCircuitState(TestKey));
        }

        [Test]
        public void GetCircuitState_IfKeyDoesExist_ThenReturnResult()
        {
            var policy = GetCircuitBreakerPolicy();
            _sut.Registry.Add(TestKey, policy);

            var state = _sut.GetCircuitState(TestKey);
            Assert.AreEqual(policy.CircuitState, state);
        }

        [Test]
        public void GetLastException_IfKeyDoesNotExist_ThenThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _sut.GetLastException(TestKey));
        }

        [Test]
        public void GetLastException_IfKeyDoesExist_ThenReturnResult()
        {
            var policy = GetCircuitBreakerPolicy();
            _sut.Registry.Add(TestKey, policy);

            var exception = _sut.GetLastException(TestKey);
            Assert.AreEqual(policy.LastException, exception);
        }

        [Test]
        public void TryIsolate_IfKeyDoesNotExist_ThenThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _sut.TryIsolate(TestKey));
        }

        [Test]
        public void TryIsolate_IfKeyDoesExist_ThenReturnResult()
        {
            var policy = GetCircuitBreakerPolicy();
            _sut.Registry.Add(TestKey, policy);

            var result = _sut.TryIsolate(TestKey);
            Assert.IsTrue(result);
        }

        [Test]
        public void TryReset_IfKeyDoesNotExist_ThenThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _sut.TryReset(TestKey));
        }

        [Test]
        public void TryReset_IfKeyDoesExist_ThenReturnResult()
        {
            var policy = GetCircuitBreakerPolicy();
            _sut.Registry.Add(TestKey, policy);

            var result = _sut.TryReset(TestKey);
            Assert.IsTrue(result);
        }
    }
}