using CoreEntities.Blackboard;
using Framework.BehaviorTree.Test.Decorators;
using Perception;

namespace Framework.BehaviorTree.Test.Sensors; 

public class EnemyInRangeSensor: Sensor {

    public Enemy? Enemy { get; set; }
    
    public EnemyInRangeSensor(Blackboard blackboard) : base(blackboard)
    {
    }
    public override void Sense(float delta)
    {
        _blackboard.Set("Enemy", Enemy);
    }
}