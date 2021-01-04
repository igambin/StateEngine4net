using System;
using System.Linq.Expressions;

namespace StateEngine4net.Shared.Interfaces
{
    public interface IState<TState>
    {
        TState T_Error(TState previousState, Expression<Func<TState, TState>> attemptedTransition, Exception exception);
        TState UndefinedTransition(string transition);
        TState FailedTransition(string transition);
    }
}