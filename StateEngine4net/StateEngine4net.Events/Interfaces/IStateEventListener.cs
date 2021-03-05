namespace StateEngine4net.Events.Interfaces
{
    public interface IStateEventListener<TEvent> where TEvent : IStateEvent
    {
        Task Notify(TEvent domainEvent);
    }
}