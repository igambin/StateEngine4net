using System;
using System.Linq.Expressions;
using StateEngine4net.Core.Interfaces;

namespace StateEngine4net.Core.Models
{
    public class TransitionDefinition<TEntity, TState, TStateEnum>
        where TEntity : IStatedEntity<TState>, new()
        where TState : IState<TState, TStateEnum>
    {
        public TransitionDefinition(
              TState fromState
            , Expression<Func<TState, TState>> stateTransitionOnSuccess
            , Func<IStateHandler<TEntity>> onTransitioning
            , Expression<Func<TState, TState>> stateTransitionOnFailed
            , Func<IStateHandler<TEntity>> onTransitionFailed
            )
        {
            From = fromState;
            StateTransitionOnSuccess = stateTransitionOnSuccess;
            OnTransitioning = onTransitioning;
            StateTransitionOnFailed = stateTransitionOnFailed;
            OnTransitionFailed = onTransitionFailed;
        }

        public string Name => $"{From.GetType().Name}.{StateTransitionOnSuccess.TransitionName<TState, TStateEnum>()}";
        public TState From { get; }
        public Expression<Func<TState, TState>> StateTransitionOnSuccess { get; }
        public Func<IStateHandler<TEntity>> OnTransitioning { get; }
        public Expression<Func<TState, TState>> StateTransitionOnFailed { get; }
        public Func<IStateHandler<TEntity>> OnTransitionFailed { get; }
    }

}
