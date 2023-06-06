using CoreEntities.Actions;

namespace DecisionMaking.BehaviorTree.Task; 

public abstract class TreeTask {
    public abstract bool Has(AiAction? action);
    public abstract AiAction? Run();
}