using System.Collections.Generic;
using System.Threading.Tasks;
using StateEngine4net.Core.Interfaces;
using StateEngine4net.Core.Models;
using StateEngine4net.Core.Transitions;
using StateEngine4net.Core.Transitions.Interfaces;
using StateEngine4net.Events;
using StateEngine4net.Events.Interfaces;

namespace StateEngine4net.Core
{
    public abstract class StateEngine<TEntity, TState, TStateEnum> : IStateEngine<TEntity, TState, TStateEnum>
        where TEntity : class, IStatedEntity<TState>, new()
        where TState : IState<TState, TStateEnum>
    {
        private readonly IStateEventBus<StateChangedEvent<TEntity>> _stateEventBus;

        protected StateEngine(IStateEventBus<StateChangedEvent<TEntity>> stateEventBus)
        {
            _stateEventBus = stateEventBus;
        }

        public abstract List<TransitionDefinition<TEntity, TState, TStateEnum>> Transitions { get; }

        public IStateTransitionBuilder<TEntity, TState, TStateEnum> For(TEntity statedEntity)
            => new StateTransitionBuilder<TEntity, TState, TStateEnum>(statedEntity, Transitions, this);

        public async Task NotifyStateChange(TEntity statedEntity)
        {
            await _stateEventBus
                .Notify(new StateChangedEvent<TEntity>
                    {
                        Entity = statedEntity
                    })
                .ConfigureAwait(false);
        }
    }
}



