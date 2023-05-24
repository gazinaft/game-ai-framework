using DecisionMaking.FiniteStateMachine.Transitions;

namespace DecisionMaking.FiniteStateMachine.States;

public class State
{
    public StateLogic Logic { get; }

    private readonly List<Transition> _edges = new();

    public State AddTransition(Transition tr)
    {
        _edges.Add(tr);
        return this;
    }

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