﻿using System;

namespace StateEngine4net.Core.Exceptions
{
    public class TransitionNotSpecifiedException : Exception
    {
        public TransitionNotSpecifiedException() : base() { }

        public TransitionNotSpecifiedException(string message) : base(message) { }

        public TransitionNotSpecifiedException(Exception ex) : base("Transition failed!", ex) { }

        public TransitionNotSpecifiedException(string message, Exception ex) : base(message, ex) { }
    }

}