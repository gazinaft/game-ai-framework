using CoreEntities.Blackboard;

namespace Perception; 

public abstract class Sensor {
    protected readonly Blackboard _blackboard;
    protected Sensor(Blackboard blackboard)
    {
        _blackboard = blackboard;

    }
    public abstract void Sense();
}
