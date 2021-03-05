using StateEngine4net.Events.Interfaces;

namespace StateEngine4net.Events
{
    public class StateEventBus<TEvent> : IStateEventBus<TEvent> where TEvent: IStateEvent
    {

        private readonly IList<IStateEventListener<TEvent>> _registeredListeners;

        public StateEventBus()
        {
            _registeredListeners = new List<IStateEventListener<TEvent>>();
        }

        public void RegisterForEvent(IStateEventListener<TEvent> eventListener)
        {
            if (!_registeredListeners.Contains(eventListener))
            {
                _registeredListeners.Add(eventListener);
            }
        }

        public async Task Notify(TEvent stateEvent) 
        {
            foreach (var stateEventListener in _registeredListeners)
            {
                try
                {
                    await stateEventListener.Notify(stateEvent).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    // TODO log Exception
                }
            }
        }
    }
}