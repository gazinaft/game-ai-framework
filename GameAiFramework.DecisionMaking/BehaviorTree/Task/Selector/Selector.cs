using CoreEntities.Actions;

namespace DecisionMaking.BehaviorTree.Task.Selector; 

public class Selector: TreeTask {
    private readonly List<TreeTask> _children;
    
    public Selector(List<TreeTask> children)
    {
        _children = children;
    }

    public override bool Has(AiAction? action)
    {
        return _children.Any(c => c.Has(action));
    }
    public override AiAction? Run()
    {
        foreach (var treeTask in _children)
        {
            if (treeTask.Run() is {} t )
            {
                return t;
            }
        }

        return  null;
    }

    public override string ToString()
    {
        return "Selector" + '\n' + _children.Select(x => x.ToString()).Aggregate((x, y) => x + '\n' + y);
    }
}