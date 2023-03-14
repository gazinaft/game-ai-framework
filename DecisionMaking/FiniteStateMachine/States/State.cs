namespace DecisionMaking.FiniteStateMachine.States;

public class State
{
    public StateLogic Logic { get; }

    private List<Transition> _edges = new List<Transition>();

    public State(StateLogic logic)
    {
        Logic = logic;
    }

    public State? GetNextState()
    {
        return _edges
            .FirstOrDefault(s => s.IsTraversable())
            ?.Traverse();
    }

    public State? GetInterruption()
    {
        return _edges.
            FirstOrDefault(s => s.IsInterruption && s.IsTraversable())
            ?.Traverse();
    }
}