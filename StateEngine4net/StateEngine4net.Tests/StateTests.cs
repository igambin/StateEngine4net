using NUnit.Framework;
using StateEngine4net.Core.Exceptions;

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