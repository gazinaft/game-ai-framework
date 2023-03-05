namespace DecisionMaking.FiniteStateMachine;

public class EdgeChain
{
    private EdgeChain Next { get; }
    
    public EdgeChain(EdgeChain next)
    {
        Next = next;
    }

    public virtual State ChooseNextState(List<Edge> edges)
    {
        return Next.ChooseNextState(edges);
    }
}