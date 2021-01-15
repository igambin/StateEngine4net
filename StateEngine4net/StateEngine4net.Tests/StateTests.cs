using System;
using System.Linq.Expressions;
using NUnit.Framework;
using StateEngine4net.Shared;
using StateEngine4net.Shared.Exceptions;
using StateEngine4net.Shared.Interfaces;
using StateEngine4net.Tests.StateEngine;

namespace StateEngine4net.Tests
{
    public class StateTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void InitialStateName()
        {
            var state = new StatedTestObjectStates.Initial();
            Assert.AreEqual("Initial", state.ToString());
        }

        [Test]
        public void TechnicalErrorStateName()
        {
            var state = new StatedTestObjectStates.TechnicalError();
            Assert.AreEqual("TechnicalError", state.ToString());
        }

        [Test]
        public void StateCanThrowUndefinedTransitionException()
        {
            var state = new StatedTestObjectStates.Initial();
            Assert.Throws<UndefinedTransitionException>(() => state.UndefinedTransition("Test"));
        }

        [Test]
        public void StateCanThrowTransitionFailedException()
        {
            var state = new StatedTestObjectStates.Initial();
            Assert.Throws<TransitionFailedException>(() => state.FaíledTransition("Test"));
        }

    }
}