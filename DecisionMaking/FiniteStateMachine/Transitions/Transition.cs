using DecisionMaking.FiniteStateMachine.States;

namespace DecisionMaking.FiniteStateMachine.Transitions;

public class Transition
{
    private State _to;
    private TransitionLogic? _logic;
    public bool IsInterruption { get; }
    

    public Transition(State to, TransitionLogic? logic, bool isInterruption)
    {
        _to = to;
        _logic = logic;
        IsInterruption = isInterruption;
    }

    public bool IsTraversable()
    {
        return _logic is null || _logic.CanTraverse;
    }

    public State Traverse()
    {
        return _to;
    }
}
