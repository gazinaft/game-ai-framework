using CoreEntities.Blackboard;
using Framework.BehaviorTree.Test.Leafs;
using Perception;
using System.Numerics;

namespace Framework.BehaviorTree.Test.Sensors; 

public class AiPositionSensor: Sensor {
    public Vector2? AnotherPoint { get; set; }
    
    public AiPositionSensor(Blackboard blackboard) : base(blackboard)
    {
    }
    
    public override void Sense()
    {
        _blackboard.Set("Position", AnotherPoint ?? GoToPointLeaf.PointA);
    }
}