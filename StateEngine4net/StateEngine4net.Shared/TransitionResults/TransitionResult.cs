using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using StateEngine4net.Core.Exceptions;
using StateEngine4net.Core.TransitionResults.Interfaces;

namespace StateEngine4net.Core.TransitionResults
{
    public abstract class TransitionResult<TTransitionResult> : ITransitionResult
        where TTransitionResult : class, ITransitionResult
    {
        protected TransitionResult()
        {
            MessageArgs = new Dictionary<string, string>();
        }

        public string PreviousState { get; set; }
        public string AttemptedTransition { get; set; }
        public abstract bool Success { get; }

        [IgnoreDataMember]
        public virtual Exception Exception { get; set; }
        
        public string MessageKey { get; set; }
        public IDictionary<string, string> MessageArgs { get; }

        public ITransitionResult AddMessageArgs(IDictionary<string, string> args)
        {
            args.ToList().ForEach(MessageArgs.Add);
            return this as TTransitionResult;
        }

        public ITransitionResult AddMessageArg(string key, string value)
        {
            MessageArgs.Add(key, value);
            return this as TTransitionResult;
        }

        private string _exceptionType;
        public string ExceptionType
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_exceptionType))
                {
                    _exceptionType = this.Exception?.GetType().Name;
                }
                return _exceptionType;
            }
            set => _exceptionType = value;
        }

        private string _exceptionMessage;
        public string ExceptionMessage
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_exceptionMessage))
                {
                    _exceptionMessage = this.Exception?.Messages();
                }
                return _exceptionMessage;
            }
            set => _exceptionMessage = value;
        }

    }
}