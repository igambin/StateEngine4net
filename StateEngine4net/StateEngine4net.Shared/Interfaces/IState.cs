using System;
using System.Linq.Expressions;

namespace IG.SimpleStateWithActions.StateEngineShared.Interfaces
{
    public interface IState<TState>
    {
        TState T_Error(TState previousState, Expression<Func<TState, TState>> attemptedTransition, Exception exception);
        TState UndefinedTransition(string transition);
        TState Fa�ledTransition(string transition);
    }
}