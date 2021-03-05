using System;

namespace StateEngine4net.Core.TransitionResults.Interfaces
{
    public interface ITransitionExecutionResult : ITransitionResult
    {
        void SetException(Exception exception);
    }
}