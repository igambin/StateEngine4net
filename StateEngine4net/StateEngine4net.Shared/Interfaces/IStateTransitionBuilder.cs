﻿using System;
using System.Linq.Expressions;

namespace StateEngine4net.Shared.Interfaces
{
    public interface IStateTransitionBuilder<TEntity, TState, TStateEnum> : IStateTransition<TEntity, TState, TStateEnum>
        where TEntity : IStatedEntity<TState>, new()
        where TState : IState<TState, TStateEnum>
    {
        IStateTransitionValidator<TEntity, TState, TStateEnum> InvokeTransition(Expression<Func<TState, TState>> transition);
    }
}