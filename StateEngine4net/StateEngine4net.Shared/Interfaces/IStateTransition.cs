﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace StateEngine4net.Shared.Interfaces
{
    public interface IStateTransition<TEntity, TState>
        where TEntity : IStatedEntity<TState>, new()
        where TState : IState<TState>
    {
        TEntity StatedEntity { get; }
        List<Transition<TEntity, TState>> Transitions { get; }
        Expression<Func<TState, TState>> TransitionToInvoke { get; }
        Func<TEntity, bool> PreCondition { get; }
        Action<Exception> ActionOnError { get; }
        Action ActionOnSuccess { get; }
        Action ActionOnFailed { get; }

    }
}