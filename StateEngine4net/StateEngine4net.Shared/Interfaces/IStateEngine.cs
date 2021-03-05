using System.Collections.Generic;
using System.Threading.Tasks;
using StateEngine4net.Core.Models;
using StateEngine4net.Core.Transitions.Interfaces;

namespace StateEngine4net.Core.Interfaces
{
    public interface IStateEngine<TEntity, TState, TStateEnum>
        where TEntity : IStatedEntity<TState>, new()
        where TState : IState<TState, TStateEnum>
    {
        List<TransitionDefinition<TEntity, TState, TStateEnum>> Transitions { get; }
        IStateTransitionBuilder<TEntity, TState, TStateEnum> For(TEntity statedEntity);

        Task NotifyStateChange(TEntity statedEntity);
    }
}