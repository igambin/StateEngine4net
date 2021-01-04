using System;
using System.Linq.Expressions;
using StateEngine4net.Shared.Exceptions;
using StateEngine4net.Shared.Interfaces;

namespace StateEngine4net.Shared
{
    public abstract class State<TState> : IState<TState>
    {
        public abstract TState T_Error(TState previousState, Expression<Func<TState, TState>> attemptedTransition, Exception exception);
        public override string ToString() => $"{GetType().Name}";
        public TState UndefinedTransition(string transition) => throw new UndefinedTransitionException(transition, GetType().Name);
        public TState FailedTransition(string transition) => throw new TransitionFailedException(transition, GetType().Name);
    }
}