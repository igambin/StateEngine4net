using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using StateEngine4net.Core.Interfaces;
using StateEngine4net.Core.Models;
using StateEngine4net.Core.Transitions.Interfaces;

namespace StateEngine4net.Core.Transitions
{
    public class StateTransitionBuilder<TEntity, TState, TStateEnum> : StateTransition<TEntity, TState, TStateEnum>, IStateTransitionBuilder<TEntity, TState, TStateEnum>
        where TEntity : class, IStatedEntity<TState>, new()
        where TState : IState<TState, TStateEnum>
    {

        public StateTransitionBuilder(TEntity statedEntity, List<TransitionDefinition<TEntity, TState, TStateEnum>> transitions,
            IStateEngine<TEntity, TState, TStateEnum> stateEngine)
        {
            if (transitions == null) throw new ArgumentNullException(nameof(transitions));
            if (!transitions.Any()) throw new ArgumentException(@"List may not be empty", nameof(transitions));
            StateEngine = stateEngine;
            StatedEntity = statedEntity ?? throw new ArgumentNullException(nameof(statedEntity));
            Transitions = transitions.ToList();
        }

        public IStateTransitionRunner<TEntity, TState, TStateEnum> InvokeTransition(Expression<Func<TState, TState>> transition)
        {
            if (transition == null) throw new ArgumentNullException(nameof(transition));
            return new StateTransitionRunner<TEntity, TState, TStateEnum>(this, transition);
        }
    }
}
