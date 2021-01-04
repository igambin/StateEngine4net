using System.Collections.Generic;
using StateEngine4net.Shared.Interfaces;

namespace StateEngine4net.Shared
{
    public abstract class StateEngine<TEntity, TState> : IStateEngine<TEntity, TState>
        where TEntity : class, IStatedEntity<TState>, new()
        where TState : IState<TState>
    {

        public abstract List<Transition<TEntity, TState>> Transitions { get; }

        public IStateTransitionBuilder<TEntity, TState> For(TEntity statedEntity)
            => new StateTransitionBuilder<TEntity, TState>(statedEntity, Transitions);

    }
}



