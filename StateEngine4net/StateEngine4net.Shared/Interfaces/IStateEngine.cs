using System.Collections.Generic;

namespace StateEngine4net.Shared.Interfaces
{
    public interface IStateEngine<TEntity, TState>
        where TEntity : IStatedEntity<TState>, new()
        where TState : IState<TState>
    {
        List<Transition<TEntity, TState>> Transitions { get; }
        IStateTransitionBuilder<TEntity, TState> For(TEntity statedEntity);
    }
}