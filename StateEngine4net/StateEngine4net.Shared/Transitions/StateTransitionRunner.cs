using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using StateEngine4net.Core.Exceptions;
using StateEngine4net.Core.Interfaces;
using StateEngine4net.Core.Models;
using StateEngine4net.Core.TransitionResults;
using StateEngine4net.Core.TransitionResults.Interfaces;
using StateEngine4net.Core.Transitions.HandlerInterfaces;
using StateEngine4net.Core.Transitions.Interfaces;

namespace StateEngine4net.Core.Transitions
{
    public class StateTransitionRunner<TEntity, TState, TStateEnum> 
        : StateTransition<TEntity, TState, TStateEnum>, 
          IStateTransitionRunner<TEntity, TState, TStateEnum>
        where TEntity : class, IStatedEntity<TState>, new()
        where TState : IState<TState, TStateEnum>
    {

        public StateTransitionRunner(
            IStateTransitionBuilder<TEntity, TState, TStateEnum> builder, 
            Expression<Func<TState, TState>> transitionToInvoke
            )
        {
            StatedEntity = builder?.StatedEntity;
            Transitions = builder?.Transitions;
            TransitionToInvoke = transitionToInvoke;
            StateEngine = builder?.StateEngine;
        }

        public async Task<ITransitionValidationResult> IsPreconditionOk()
        {
            if (TransitionToInvoke == null) throw new TransitionNotSpecifiedException();
            var transition = GetRequestedTransition;
            IStateHandler<TEntity> transitionHandler = transition.OnTransitioning.Invoke();
            if (transitionHandler is IPrevalidation<TEntity> prevalidation)
            {
                return await prevalidation.OnValidating(StatedEntity).ConfigureAwait(false);
            }
            return new TransitionValidationSuccessful();
        }

        public async Task<bool> IsTransitionAllowed()
        {
            try
            {
                if (TransitionToInvoke == null) throw new TransitionNotSpecifiedException();
                var transitionExists = GetRequestedTransition != null;
                var preConditionSuccessful = await IsPreconditionOk().ConfigureAwait(false);
                return transitionExists && (preConditionSuccessful is TransitionValidationSuccessful);
            }
            catch
            {
                return false;
            }
        } 



        public IStateTransitionRunner<TEntity, TState, TStateEnum> OnSuccess(Action<TEntity, IState<TState, TStateEnum>> onSuccess)
        {
            ActionOnSuccess = onSuccess;
            return this;
        }

        public IStateTransitionRunner<TEntity, TState, TStateEnum> OnFailed(Action<TEntity, IState<TState, TStateEnum>> onFailed)
        {
            ActionOnFailed = onFailed;
            return this;
        }

        public IStateTransitionRunner<TEntity, TState, TStateEnum> OnError(Action<TEntity, IState<TState, TStateEnum>> onError)
        {
            ActionOnError = onError;
            return this;
        }

        private TransitionDefinition<TEntity, TState, TStateEnum> GetRequestedTransition => Transitions.FindTransition(StatedEntity, TransitionToInvoke);

        public async Task<TState> Execute()
        {
            var previousState = StatedEntity.State;
            if (TransitionToInvoke == null) throw new TransitionNotSpecifiedException();

            try
            {
                var transition = GetRequestedTransition;
                IStateHandler<TEntity> transitionHandler = transition?.OnTransitioning?.Invoke();

                var validationResult = await IsPreconditionOk().ConfigureAwait(false); 

                if (validationResult is TransitionValidationFailed failedResult && transitionHandler != null)
                {
                    StatedEntity.State.InitFailedTransitionResult(
                        previousState,
                        TransitionToInvoke,
                        failedResult);

                    if (transitionHandler is IPrevalidation<TEntity> pv)
                    {
                        await pv.OnValidationFailed(StatedEntity, failedResult).ConfigureAwait(false);
                }
                    return StatedEntity.State;
                }

                ITransitionExecutionResult transitionResult = null;
                Exception transitionException = null;

                if (transition.OnTransitioning != null)
                {
                    try
                    {
                        transitionHandler = transition.OnTransitioning.Invoke();
                        if (transitionHandler is IBeforeTransition<TEntity> bth)
                        {
                            await bth.OnPrepareTransition(StatedEntity).ConfigureAwait(false);
                    }
                        transitionResult = await transitionHandler.ExecuteTransition(StatedEntity).ConfigureAwait(false);
                    }
                    catch (Exception e)
                    {
                        transitionException = e;
                        IsTransitionSuccessful = false;
                        transitionResult = CreateFailedResult(previousState, TransitionToInvoke, "TransitionFailed", e);
                    }
                }

                if(transitionResult is TransitionSuccessful successfulResult)
                {
                    StatedEntity.State = transition.StateTransitionOnSuccess.Compile()(StatedEntity.State);
                    if (transitionHandler is IAfterSuccessfulTransition<TEntity> ast)
                    {
                        await ast.OnSuccessfulTransition(StatedEntity, successfulResult).ConfigureAwait(false);
                    }

                    StatedEntity.State.InitSuccessfulTransitionResult(successfulResult);
                    ActionOnSuccess?.Invoke(StatedEntity, StatedEntity.State);
                    await StateEngine.NotifyStateChange(StatedEntity).ConfigureAwait(false);
                    return StatedEntity.State;
                }

                if (transitionHandler is IAfterFailedTransition<TEntity> aft)
                {
                    await aft.OnFailedTransition(StatedEntity, transitionResult as TransitionFailed).ConfigureAwait(false);
                }

                if (transition.OnTransitionFailed == null)
                {
                    throw new TransitionFailedException(transition.StateTransitionOnSuccess.TransitionName<TState, TStateEnum>(),
                        $"{previousState}", transitionException);
                }

                throw new TransitionRollbackFailedException(
                    transition.StateTransitionOnFailed?.TransitionName<TState, TStateEnum>() 
                    ?? "[rollback undefined]", $"{previousState}", transitionException);
            }
            catch (Exception ex)
            {
                StatedEntity.State.InitFailedTransitionResult(previousState, TransitionToInvoke, "TransitionExecutionFailed", ex);
                StatedEntity.State.TransitionResult.AddMessageArg("", "");
                ActionOnError?.Invoke(StatedEntity, StatedEntity.State);
                return StatedEntity.State;
            }

        }

        public bool IsTransitionSuccessful { get; private set; }
        public bool IsRollbackSuccessful { get; private set; }


        private TransitionFailed CreateFailedResult(TState previousState,
            Expression<Func<TState, TState>> attemptedTransition, string messageKey = null, Exception exception = null)
        {
            var body = attemptedTransition?.Body as MethodCallExpression;
            return new TransitionFailed
            {
                PreviousState = previousState.GetType().Name,
                AttemptedTransition = body?.Method.Name ?? "",
                MessageKey = messageKey,
                Exception = exception,
            };
        }

        private TransitionSuccessful CreateSuccessfulResult(TState previousState,
            Expression<Func<TState, TState>> attemptedTransition, string messageKey = null)
        {
            var body = attemptedTransition?.Body as MethodCallExpression;
            return new TransitionSuccessful
            {
                PreviousState = previousState.GetType().Name,
                AttemptedTransition = body?.Method.Name ?? "",
                MessageKey = messageKey,
            };
        }

    }
}