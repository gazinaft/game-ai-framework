using CoreEntities.Blackboard;

namespace DecisionMaking.BehaviorTree.Task.Decorator; 

public abstract class DecoratorLogic {
    protected Blackboard _blackboard { get; }
    
    public DecoratorLogic(Blackboard blackboard)
    {
        _blackboard = blackboard;

    }
    
    public abstract bool CanRun();
}