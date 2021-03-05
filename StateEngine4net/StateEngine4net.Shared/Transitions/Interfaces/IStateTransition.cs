using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using StateEngine4net.Core.Interfaces;
using StateEngine4net.Core.Models;

namespace StateEngine4net.Core.Transitions.Interfaces
{
    public interface IStateTransition<TEntity, TState, TStateEnum>
        where TEntity : IStatedEntity<TState>, new()
        where TState : IState<TState, TStateEnum>
    {
        TEntity StatedEntity { get; }
        List<TransitionDefinition<TEntity, TState, TStateEnum>> Transitions { get; }
        Expression<Func<TState, TState>> TransitionToInvoke { get; }
        Action<TEntity, IState<TState, TStateEnum>> ActionOnError { get; set; }
        Action<TEntity, IState<TState, TStateEnum>> ActionOnSuccess { get; set; }
        Action<TEntity, IState<TState, TStateEnum>> ActionOnFailed { get; set; }
        IStateEngine<TEntity, TState, TStateEnum> StateEngine { get; set; }


    }
}