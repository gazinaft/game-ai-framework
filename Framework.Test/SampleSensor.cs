using CoreEntities.Blackboard;
using Perception;

namespace Framework.Test;

public class SampleSensor : Sensor
{
    private Blackboard _global;

    public SampleSensor(Type type, string id, Blackboard global) : base(type, id)
    {
        _global = global;
    }

    public override ActorStub? Sense()
    {
        return _global.Get<ActorStub>("Enemy")?.Value;
    }
}