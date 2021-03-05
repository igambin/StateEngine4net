using System;
using System.Linq.Expressions;
using StateEngine4net.Core.Interfaces;

namespace StateEngine4net.Core.Transitions.Interfaces
{
    public interface IStateTransitionBuilder<TEntity, TState, TStateEnum> : IStateTransition<TEntity, TState, TStateEnum>
        where TEntity : IStatedEntity<TState>, new()
        where TState : IState<TState, TStateEnum>
    {
        IStateTransitionRunner<TEntity, TState, TStateEnum> InvokeTransition(Expression<Func<TState, TState>> transition);
    }
}