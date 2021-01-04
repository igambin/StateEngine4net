using System;

namespace StateEngine4net.Shared.Interfaces
{
    public interface IStateTransitionRunner<TEntity, TState> : IStateTransition<TEntity, TState>
        where TEntity : IStatedEntity<TState>, new()
        where TState : IState<TState>
    {
        bool IsTransitionAllowed();
        IStateTransitionRunner<TEntity, TState> OnError(Action<Exception> onError);
        IStateTransitionRunner<TEntity, TState> OnSuccess(Action onSuccess);
        IStateTransitionRunner<TEntity, TState> OnFailed(Action onFailed);
        TState Execute();
    }
}