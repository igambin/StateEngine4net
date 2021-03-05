using System;
using System.Threading.Tasks;
using StateEngine4net.Core.Interfaces;
using StateEngine4net.Core.TransitionResults.Interfaces;

namespace StateEngine4net.Core.Transitions.Interfaces
{
    public interface IStateTransitionRunner<TEntity, TState, TStateEnum> : IStateTransition<TEntity, TState, TStateEnum>
        where TEntity : IStatedEntity<TState>, new()
        where TState : IState<TState, TStateEnum>
    {
        Task<bool> IsTransitionAllowed();
        IStateTransitionRunner<TEntity, TState, TStateEnum> OnError(Action<TEntity, IState<TState, TStateEnum>> onError);
        IStateTransitionRunner<TEntity, TState, TStateEnum> OnSuccess(Action<TEntity, IState<TState, TStateEnum>> onSuccess);
        IStateTransitionRunner<TEntity, TState, TStateEnum> OnFailed(Action<TEntity, IState<TState, TStateEnum>> onFailed);
        Task<TState> Execute();
        bool IsTransitionSuccessful { get; }
        bool IsRollbackSuccessful { get; }
        Task<ITransitionValidationResult> IsPreconditionOk();

    }
}