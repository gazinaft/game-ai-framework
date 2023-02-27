namespace DecisionMaking.FiniteStateMachine;

public class RandomEdgeChain: EdgeChain
{
    public override State ChooseNextState(List<Edge> edges)
    {
        var rng = new Random();

        return edges[rng.Next(0, edges.Count)].Traverse();
    }
}