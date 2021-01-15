using System.Collections.Generic;

namespace StateEngine4net.Shared.Interfaces
{
    public interface IStateEngine<TEntity, TState, TStateEnum>
        where TEntity : IStatedEntity<TState>, new()
        where TState : IState<TState, TStateEnum>
    {
        List<Transition<TEntity, TState, TStateEnum>> Transitions { get; }
        IStateTransitionBuilder<TEntity, TState, TStateEnum> For(TEntity statedEntity);
    }
}