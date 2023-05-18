using CoreEntities.Actions;

namespace CoreEntities.DecisionMaker; 

public abstract class DecisionMaker {
    public abstract AiAction RequestAction();
    public virtual void OnActionSet(AiAction setAction) {}
    public virtual void OnActionInterrupted(AiAction interruptAction) {}
    public virtual void OnActionComplete(AiAction completedAction) {}

    public abstract AiAction? Update();
}