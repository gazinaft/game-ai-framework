namespace DecisionMaking.FiniteStateMachine;

public class StateLogic
{
    public event Action Processed;

    public Object Actor;

    public virtual void Ready()
    {
    }

    public virtual void Process()
    {
    }
}