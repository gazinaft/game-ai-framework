using CoreEntities.Actions;

namespace DecisionMaking.FiniteStateMachine.States;

public abstract class StateLogic : AiAction
{
    public void SetInterrupted()
    {
        Interrupt = true;
    }

    protected StateLogic(int priority, float expireTime) : base(priority, expireTime)
    {
    }
}