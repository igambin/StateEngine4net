using StateEngine4net.Core.Interfaces;

namespace StateEngine4net.Tests.TestModel
{
    public class StatedTestObject : IStatedEntity<IStatedTestObjectState>
    {
        public IStatedTestObjectState State { get; set; }
    }
}
