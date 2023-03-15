using CoreEntities.Blackboard;

namespace Perception; 

public abstract class Sensor {
    protected Sensor(Blackboard blackboard)
    {
        
    }
    
    public abstract void Sense();
}