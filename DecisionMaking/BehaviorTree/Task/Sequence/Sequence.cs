using CoreEntities.Actions;

namespace DecisionMaking.BehaviorTree.Task.Sequence;

public class Sequence : TreeTask {
    private readonly List<TreeTask> _children;
    private int _currentTask;

    public Sequence(List<TreeTask> children, BehaviorTree behaviorTree)
    {
        _children = children;
        behaviorTree.ActionInterrupted += (_) => _currentTask = 0;
        behaviorTree.ActionComplete += (t) => {
            if (Has(t))
                _currentTask += 1;
        };
    }
    public override bool Has(AiAction? action)
    {
        return _children.Any(c => c.Has(action));
    }
    public override AiAction? Run()
    {
        if (_currentTask == _children.Count)
            _currentTask = 0;

        var task = _children[_currentTask];
        if (task.Run() is {} t)
        {
            return t;
        }
        
        return null;
    }

    public override string ToString()
    {
        return "Sequence" + '\n' + _children.Select(x => x.ToString()).Aggregate((x, y) => x + '\n' + y);

    }
}