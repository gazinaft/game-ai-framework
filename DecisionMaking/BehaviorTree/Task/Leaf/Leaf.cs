using CoreEntities.Actions;

namespace DecisionMaking.BehaviorTree.Task.Leaf;

public class Leaf : TreeTask {
    private LeafLogic _leafLogic;
    
    public Leaf(LeafLogic leafLogic)
    {
        _leafLogic = leafLogic;
    }

    public override bool Has(AiAction? action)
    {
        return _leafLogic == action;
    }
    public override AiAction? Run()
    {
        return _leafLogic;
    }

    public override string ToString()
    {
        return "Leaf";
    }
}