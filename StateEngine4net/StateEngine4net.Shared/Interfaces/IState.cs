using System;
using System.Linq.Expressions;
using StateEngine4net.Core.TransitionResults;
using StateEngine4net.Core.TransitionResults.Interfaces;

namespace StateEngine4net.Core.Interfaces
{
    public interface IState<TState, TStateEnum>
    {
        TStateEnum Name { get; }
        TState UndefinedTransition(string transition);
        TState FailedTransition(string transition);
        bool Equals<TExpectedState>();


        void InitFailedTransitionResult(TState previousState, Expression<Func<TState, TState>> attemptedTransition, string messageKey = null, Exception exception = null);

        void InitFailedTransitionResult(TState previousState, Expression<Func<TState, TState>> attemptedTransition, TransitionValidationFailed transitionValidationFailed);

        void InitFailedTransitionResult(string previousState, string attemptedTransition, string messageKey = null, Exception exception = null);

        void InitFailedTransitionResult(TransitionFailed result);

        void InitSuccessfulTransitionResult(TState previousState, Expression<Func<TState, TState>> attemptedTransition, string messageKey = null);

        void InitSuccessfulTransitionResult(string previousState, string attemptedTransition, string messageKey = null);

        void InitSuccessfulTransitionResult(TransitionSuccessful result);

        ITransitionExecutionResult TransitionResult { get; }
    }
}