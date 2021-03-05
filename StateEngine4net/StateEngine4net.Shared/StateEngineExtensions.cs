using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using StateEngine4net.Core.Exceptions;
using StateEngine4net.Core.Interfaces;
using StateEngine4net.Core.Models;

namespace StateEngine4net.Core
{
    public static class StateEngineExtensions
    {
        public static string TransitionName<TState, TStateEnum>(this Expression<Func<TState, TState>> transition)
            where TState : IState<TState, TStateEnum>
        {
            var transitionMember = transition?.Body as MemberExpression;
            return transitionMember?.Member.Name ?? "[undefined member]";
        }

        public static string ActualStateName<TEntity, TState, TStateEnum>(this TEntity entity)
            where TEntity : IStatedEntity<TState>, new()
            where TState : IState<TState, TStateEnum> 
            => $"{entity.State}";

        public static TransitionDefinition<TEntity, TState, TStateEnum> FindTransition<TEntity, TState, TStateEnum>(
            this List<TransitionDefinition<TEntity, TState, TStateEnum>> transitions, 
            TEntity entity, 
            Expression<Func<TState, TState>> transition
        )
            where TEntity : IStatedEntity<TState>, new()
            where TState : IState<TState, TStateEnum>
        {
            TransitionDefinition<TEntity, TState, TStateEnum> requestedTransition = default;
            MethodCallExpression needleMember;
            try 
            {
                var needleType = entity.State.GetType();
                needleMember = transition?.Body as MethodCallExpression;
                if (needleMember == null)
                {
                    throw new TransitionFailedException($"Transition '{transition?.Body.ToString() ?? "[null]"}' failed on {typeof(TEntity)}");
                }

                transitions?.ForEach(t =>
                {
                    var transitionMember = t.StateTransitionOnSuccess.Body as MethodCallExpression;
                    if (needleType == t.From.GetType()
                        && needleMember.Method.Name == (transitionMember?.Method.Name ?? "[undefined member]")
                        && needleMember.Method.ReflectedType == transitionMember?.Method.ReflectedType)
                    {
                        requestedTransition = t;
                    }
                });
            }
            catch (Exception ex)
            {
                throw new TransitionFailedException(ex);
            }

            if (requestedTransition == default)
            {
                throw new UndefinedTransitionException(needleMember.Method.Name, entity.State.GetType().Name);
            }

            return requestedTransition;
        }
    }
}