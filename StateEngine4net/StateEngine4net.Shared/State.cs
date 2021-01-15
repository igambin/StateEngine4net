using System;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using StateEngine4net.Shared.Exceptions;
using StateEngine4net.Shared.Interfaces;

namespace StateEngine4net.Shared
{
    public abstract class State<TState, TStateEnum> : IState<TState, TStateEnum>
    {
        public override string ToString() => $"{GetType().Name}";
        public TState UndefinedTransition(string transition) => throw new UndefinedTransitionException(transition, GetType().Name);
        public TState FaíledTransition(string transition) => throw new TransitionFailedException(transition, GetType().Name);
        public abstract TStateEnum Name { get; }
        
        public bool Equals<TExpectedState>() => GetType() == typeof(TExpectedState);
        public abstract TState TechnicalError();

        public void Init(TState previousState, Expression<Func<TState, TState>> attemptedTransition, Exception exception = null)
        {
            var body = attemptedTransition?.Body as MethodCallExpression;
            PreviousState = previousState.GetType().Name;
            AttemptedTransition = body?.Method.Name ?? "";
            Exception = exception;
        }

        public string PreviousState { get; set; }
        public string AttemptedTransition { get; set; }
        [JsonIgnore]
        public Exception Exception { get; set; }
        public string ExceptionType
        {
            get {
                if (string.IsNullOrWhiteSpace(_exceptionType))
                {
                    _exceptionType = this.Exception?.GetType().Name;
                }

                return _exceptionType;
            }
            set => _exceptionType = value;
        }

        private string _exceptionType;
        
    }
}