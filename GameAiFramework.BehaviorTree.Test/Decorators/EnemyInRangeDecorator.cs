using CoreEntities.Blackboard;
using DecisionMaking.BehaviorTree.Task.Decorator;

namespace Framework.BehaviorTree.Test.Decorators; 

public class EnemyInRangeDecorator: DecoratorLogic {
    public EnemyInRangeDecorator(Blackboard blackboard) : base(blackboard)
    {
    }
    
    public override bool CanRun()
    {
        var enemy = _blackboard.Get<Enemy>("Enemy");

        return enemy.Value is not null;
    }
}