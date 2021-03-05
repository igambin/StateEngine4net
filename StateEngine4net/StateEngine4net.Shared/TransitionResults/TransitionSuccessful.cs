using System;
using StateEngine4net.Core.TransitionResults.Interfaces;

namespace StateEngine4net.Core.TransitionResults
{
    public class TransitionSuccessful : TransitionResult<TransitionSuccessful>, ITransitionExecutionResult
    {
        public override bool Success => true;
        public override Exception Exception => null;
        public void SetException(Exception exception) => Exception = exception;
    }
}