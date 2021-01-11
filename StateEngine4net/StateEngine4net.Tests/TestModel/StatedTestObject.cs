using System;
using System.Collections.Generic;
using System.Text;
using StateEngine4net.Shared.Interfaces;
using StateEngine4net.Tests.StateEngines;

namespace StateEngine4net.Tests.TestModel
{
    public class StatedTestObject : IStatedEntity<IStatedTestObjectState>
    {
        public IStatedTestObjectState State { get; set; }
    }
}
