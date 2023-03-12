using CoreEntities.Actions;

namespace DecisionMaking.FiniteStateMachine;

public abstract class StateLogic : AiAction
{
    public event Action Processed;

    public Object Actor;

}