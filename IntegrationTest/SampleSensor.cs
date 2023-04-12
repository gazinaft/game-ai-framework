using CoreEntities.Blackboard;
using Perception;

namespace IntegrationTest;

public class SampleSensor : Sensor
{
    private Blackboard _global;

    public SampleSensor(Type type, string id, Blackboard global) : base(type, id)
    {
        _global = global;
    }

    public override object? Sense()
    {
         return _global.Get<ActorStub>("Enemy");
    }
}