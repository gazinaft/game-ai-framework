using CoreEntities.Actions;

namespace DecisionMaking.BehaviorTree.Task.Decorator;

public class Decorator : TreeTask {
    private TreeTask _child;
    private DecoratorLogic _decoratorLogic;

    public Decorator(DecoratorLogic decoratorLogic, TreeTask child)
    {
        _child = child;
        _decoratorLogic = decoratorLogic;   
    }

    public override bool Has(AiAction? action)
    {
        return _child.Has(action);
    }

    public override AiAction? Run()
    {
        if (_decoratorLogic.CanRun())
            return _child.Run();

        return null;
    }
}