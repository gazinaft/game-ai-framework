namespace DecisionMaking.FiniteStateMachine;

public class Edge
{
    private State _from;
    private State _to;

    public Edge(State from, State to)
    {
        _from = from;
        _to = to;
    }

    public bool IsTraversable()
    {
        return true;
    }

    public State Traverse()
    {
        return _to;
    }
    
}
