using StateEngine4net.Core.TransitionResults.Interfaces;

namespace StateEngine4net.Core.TransitionResults
{
    public class TransitionValidationFailed : TransitionResult<TransitionValidationFailed>, ITransitionValidationResult
    {
        public override bool Success => false;

        public TransitionValidationFailed()
        {
            
        }

        public TransitionValidationFailed(string messageKey)
        {
            MessageKey = messageKey;
        }

        public new TransitionValidationFailed AddMessageArgs(string key, string value)
        {
            MessageArgs.Add(key, value);
            return this;
        }

    }
}