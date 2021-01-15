using System.Collections.Generic;
using StateEngine4net.Shared.Interfaces;

namespace StateEngine4net.Shared
{
    public abstract class StateEngine<TEntity, TState, TStateEnum> : IStateEngine<TEntity, TState, TStateEnum>
        where TEntity : class, IStatedEntity<TState>, new()
        where TState : IState<TState, TStateEnum>
    {

        public abstract List<Transition<TEntity, TState, TStateEnum>> Transitions { get; }

        public IStateTransitionBuilder<TEntity, TState, TStateEnum> For(TEntity statedEntity)
            => new StateTransitionBuilder<TEntity, TState, TStateEnum>(statedEntity, Transitions);

    }
}



