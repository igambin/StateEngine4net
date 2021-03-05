using StateEngine4net.Core.TransitionResults.Interfaces;

namespace StateEngine4net.Core.TransitionResults
{
    public class TransitionValidationSuccessful : TransitionResult<TransitionValidationSuccessful>,
        ITransitionValidationResult
    {
        public override bool Success => true;
    }
}