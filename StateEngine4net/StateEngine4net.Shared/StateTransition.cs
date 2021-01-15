using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using StateEngine4net.Shared.Interfaces;

namespace StateEngine4net.Shared
{
    public class StateTransition<TEntity, TState, TStateEnum> : IStateTransition<TEntity, TState, TStateEnum>
        where TEntity : class, IStatedEntity<TState>, new()
        where TState : IState<TState, TStateEnum>
    {
        public TEntity StatedEntity { get; set; }
        public List<Transition<TEntity, TState, TStateEnum>> Transitions { get; set; }
        public Expression<Func<TState, TState>> TransitionToInvoke { get; set; }
        public Func<TEntity, bool> PreCondition { get; set; }
        public Action<TEntity, IState<TState, TStateEnum>> ActionOnError { get; set; }
        public Action<TEntity, IState<TState, TStateEnum>> ActionOnSuccess { get; set; }
        public Action<TEntity, IState<TState, TStateEnum>> ActionOnFailed { get; set; }

    }
}