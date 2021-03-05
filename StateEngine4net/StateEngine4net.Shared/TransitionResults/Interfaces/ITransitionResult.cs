using System;
using System.Collections.Generic;

namespace StateEngine4net.Core.TransitionResults.Interfaces
{
    public interface ITransitionResult
    {
        string PreviousState { get; set; }
        string AttemptedTransition { get; set; }
        string MessageKey { get; set; }
        IDictionary<string, string> MessageArgs { get; }
        bool Success { get; }
        Exception Exception { get; set; }

        ITransitionResult AddMessageArgs(IDictionary<string, string> args);
        ITransitionResult AddMessageArg(string key, string value);
    }
}