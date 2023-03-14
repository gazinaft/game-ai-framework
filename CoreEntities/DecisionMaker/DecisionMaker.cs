using CoreEntities.Actions;

namespace CoreEntities.DecisionMaker; 

public abstract class DecisionMaker {
    public abstract AiAction GetNextAction();

    public abstract AiAction? SearchForInterruptions();
}