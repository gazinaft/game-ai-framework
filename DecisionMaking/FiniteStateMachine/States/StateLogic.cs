using CoreEntities.Actions;

namespace DecisionMaking.FiniteStateMachine.States;

public abstract class StateLogic : AiAction
{
    public event Action Processed;

    public void SetInterrupted()
    {
        Interrupt = true;
    }
}