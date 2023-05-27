using CoreEntities.Actions;
using CoreEntities.DecisionMaker;
using DecisionMaking.BehaviorTree.Task;

namespace DecisionMaking.BehaviorTree;

public class BehaviorTree : DecisionMaker {
    public event Action<AiAction> ActionInterrupted;
    public event Action<AiAction> ActionComplete;

    public TreeTask Root { get; set; }

    private AiAction? _lastAction;
    
    public BehaviorTree()
    {
    }

    public override void OnActionInterrupted(AiAction setAction)
    {
        ActionInterrupted?.Invoke(setAction);
    }
    
    public override void OnActionSet(AiAction setAction)
    {
        _lastAction = setAction;
    }
    
    public override void OnActionComplete(AiAction completedAction)
    {
        ActionComplete.Invoke(completedAction);
    }
    
    public override AiAction RequestAction()
    {
        var task = Update();
        _lastAction = task;
        
        if (task is null)
            throw new NullReferenceException("Task is null in Behavior Tree");

        return task;
    }
    
    public override AiAction? Update()
    {
        var task = Root.Run();

        if (_lastAction is not null && task is not null)
        {
            task.Interrupt = _lastAction != task;
        }

        return task;
    }

    public override string ToString()
    {
        return "Root: " +  Root;
    }
}