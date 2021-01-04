using System;
using System.Linq.Expressions;

namespace StateEngine4net.Shared.Interfaces
{
    public interface ITechnicalErrorState<TState>
    {
        TState PreviousState { get; set; }
        Expression<Func<TState, TState>> AttemptedTransition { get; set; }
        Exception Exception { get; set; }
    }
}