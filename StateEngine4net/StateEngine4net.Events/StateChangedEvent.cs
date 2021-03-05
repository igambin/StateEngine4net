using StateEngine4net.Events.Interfaces;

namespace StateEngine4net.Events
{
    public class StateChangedEvent<TEntity> : IStateEvent
    {
        public TEntity Entity { get; set; }
    }
}
