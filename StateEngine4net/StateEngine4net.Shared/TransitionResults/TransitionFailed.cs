using System;
using StateEngine4net.Core.TransitionResults.Interfaces;

namespace StateEngine4net.Core.TransitionResults
{
    public class TransitionFailed : TransitionResult<TransitionFailed> , ITransitionExecutionResult
    {
        public override bool Success => false;
        public void SetException(Exception exception) => Exception = exception;
    }
}