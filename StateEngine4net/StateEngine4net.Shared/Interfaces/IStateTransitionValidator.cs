﻿using System;

namespace StateEngine4net.Shared.Interfaces
{
    public interface IStateTransitionValidator<TEntity, TState> : IStateTransition<TEntity, TState>
        where TEntity : IStatedEntity<TState>, new()
        where TState : IState<TState>
    {
        IStateTransitionRunner<TEntity, TState> WithPreValidation(Func<TEntity, bool> preCondition);
        IStateTransitionRunner<TEntity, TState> WithoutPreValidation();
    }
}