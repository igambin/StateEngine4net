using System;
using System.Linq.Expressions;
using StateEngine4net.Core.Exceptions;
using StateEngine4net.Core.Interfaces;
using StateEngine4net.Core.TransitionResults;
using StateEngine4net.Core.TransitionResults.Interfaces;

namespace StateEngine4net.Core
{
    public abstract class State<TState, TStateEnum> : IState<TState, TStateEnum>
    {
        public override string ToString() => $"{GetType().Name}";
        public TState UndefinedTransition(string transition) => throw new UndefinedTransitionException(transition, GetType().Name);
        public TState FailedTransition(string transition) => throw new TransitionFailedException(transition, GetType().Name);
        public abstract TStateEnum Name { get; }
        
        public bool Equals<TExpectedState>() => GetType() == typeof(TExpectedState);

        public ITransitionExecutionResult TransitionResult { get; private set; }

        public void InitSuccessfulTransitionResult(TState previousState, Expression<Func<TState, TState>> attemptedTransition, string messageKey = null)
        {
            var body = attemptedTransition?.Body as MethodCallExpression;
            TransitionResult = new TransitionSuccessful
            {
                PreviousState = previousState.GetType().Name,
                AttemptedTransition = body?.Method.Name ?? "",
                MessageKey = messageKey,
            };
        }

        public void InitSuccessfulTransitionResult(string previousState, string attemptedTransition, string messageKey = null)
        {
            TransitionResult = new TransitionSuccessful
                {
                PreviousState = previousState,
                AttemptedTransition = attemptedTransition,
                MessageKey = messageKey,
            };
                }

        public void InitSuccessfulTransitionResult(TransitionSuccessful result)
        {
            TransitionResult = result;
        }

        public void InitFailedTransitionResult(TState previousState, Expression<Func<TState, TState>> attemptedTransition, string messageKey = null, Exception exception = null)
        {
            var body = attemptedTransition?.Body as MethodCallExpression;
            TransitionResult = new TransitionFailed
            {
                PreviousState = previousState.GetType().Name,
                AttemptedTransition = body?.Method.Name ?? "",
                MessageKey = messageKey,
                Exception = exception,
            };
            
        }

        public void InitFailedTransitionResult(TState previousState, Expression<Func<TState, TState>> attemptedTransition,
            TransitionValidationFailed transitionValidationFailed)
        {
            var body = attemptedTransition?.Body as MethodCallExpression;
            TransitionResult = new TransitionFailed
            {
                PreviousState = previousState.GetType().Name,
                AttemptedTransition = body?.Method.Name ?? "",
                MessageKey = transitionValidationFailed.MessageKey,
            };
            TransitionResult.AddMessageArgs(transitionValidationFailed.MessageArgs);
        }

        public void InitFailedTransitionResult(string previousState, string attemptedTransition, string messageKey = null, Exception exception = null)
        {
            TransitionResult = new TransitionFailed
            {
                PreviousState = previousState,
                AttemptedTransition = attemptedTransition,
                MessageKey = messageKey,
                Exception = exception,
            };
        }

        public void InitFailedTransitionResult(TransitionFailed result)
        {
            TransitionResult = result;
        }

    }
}