namespace StateEngine4net.Core.Interfaces
{
    public interface IStatedEntity<TState>
    {
        TState State { get; set; }
    }
}