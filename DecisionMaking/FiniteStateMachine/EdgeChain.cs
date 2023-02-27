namespace DecisionMaking.FiniteStateMachine;

public class EdgeChain
{
    public EdgeChain Next { get; set; }

    public virtual State ChooseNextState(List<Edge> edges)
    {
        return Next.ChooseNextState(edges);
    }
}