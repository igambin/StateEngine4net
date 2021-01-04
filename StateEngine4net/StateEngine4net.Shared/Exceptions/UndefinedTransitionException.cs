using System;

namespace StateEngine4net.Shared.Exceptions
{
    public class UndefinedTransitionException : Exception
    {
        public const string MessageTemplate = "The transition '{0}' from state '{1}' is not defined!";

        public UndefinedTransitionException(string transition, string sourceState)
            : base(string.Format(MessageTemplate, transition, sourceState)) { }

        public UndefinedTransitionException(string transition, string sourceState, Exception ex)
            : base(string.Format(MessageTemplate, transition, sourceState), ex) { }
    }
}