using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using StateEngine4net.Core.Interfaces;
using StateEngine4net.Core.Models;
using StateEngine4net.Core.Transitions.Interfaces;

namespace StateEngine4net.Core.Transitions
{
    public class StateTransition<TEntity, TState, TStateEnum> : IStateTransition<TEntity, TState, TStateEnum>
        where TEntity : class, IStatedEntity<TState>, new()
        where TState : IState<TState, TStateEnum>
    {
        public TEntity StatedEntity { get; set; }
        public List<TransitionDefinition<TEntity, TState, TStateEnum>> Transitions { get; set; }
        public Expression<Func<TState, TState>> TransitionToInvoke { get; set; }
        public Action<TEntity, IState<TState, TStateEnum>> ActionOnError { get; set; }
        public Action<TEntity, IState<TState, TStateEnum>> ActionOnSuccess { get; set; }
        public Action<TEntity, IState<TState, TStateEnum>> ActionOnFailed { get; set; }

        public IStateEngine<TEntity, TState, TStateEnum> StateEngine { get; set; }

    }
}