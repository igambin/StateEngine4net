using System;
using System.Linq.Expressions;
using NUnit.Framework;
using StateEngine4net.Shared;
using StateEngine4net.Shared.Exceptions;
using StateEngine4net.Shared.Interfaces;
using StateEngine4net.Tests.StateEngines;

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
        public void T_ErrorStateName()
        {
            var state = new StatedTestObjectStates.T_Error();
            Assert.AreEqual("T_Error", state.ToString());
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
            Assert.Throws<TransitionFailedException>(() => state.FailedTransition("Test"));
        }

        [Test]
        public void StateCanCreateT_ErrorState()
        {
            var state = new StatedTestObjectStates.Initial();
            Expression<Func<IStatedTestObjectState, IStatedTestObjectState>> transition = s => s.Cancel;
            var errorState = (ITechnicalErrorState<IStatedTestObjectState>)state.T_Error(state, transition, null);
            Assert.AreEqual(state, errorState.PreviousState);
            Assert.AreEqual(transition, errorState.AttemptedTransition);
            Assert.IsNull(errorState.Exception);
        }

    }
}