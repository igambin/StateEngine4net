namespace StateEngine4net.Events.Interfaces
{
    public interface IStateEventBus<TEvent> where TEvent : IStateEvent
    {
        void RegisterForEvent(IStateEventListener<TEvent> eventListener);
        Task Notify(TEvent stateEvent);

    }
}