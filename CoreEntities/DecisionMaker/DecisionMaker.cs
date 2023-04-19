using CoreEntities.Actions;

namespace CoreEntities.DecisionMaker; 

public abstract class DecisionMaker {
    public abstract List<AiAction?> OnActionComplete(AiAction action);

    public abstract List<AiAction> Update(float delta);
}