using CoreEntities.Blackboard;
using DecisionMaking.BehaviorTree.Task.Decorator;
using Framework.BehaviorTree.Test.Leafs;
using System.Numerics;

namespace Framework.BehaviorTree.Test.Decorators; 

public class OnPointADecorator: DecoratorLogic {

    public OnPointADecorator(Blackboard blackboard) : base(blackboard)
    {
    }
    public override bool CanRun()
    {
        return _blackboard.Get<Vector2>("Position").Value == GoToPointLeaf.PointA;
    }
}