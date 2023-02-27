namespace DecisionMaking.FiniteStateMachine;

public class StateLogic
{
    public Action Processed;

    public Object Actor;

    public virtual void Ready()
    {
    }

    public virtual void Process()
    {
    }
}