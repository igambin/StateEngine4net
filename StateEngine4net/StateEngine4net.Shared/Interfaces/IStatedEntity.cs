namespace StateEngine4net.Shared.Interfaces
{
    public interface IStatedEntity<TState>
    {
        TState State { get; set; }
    }
}