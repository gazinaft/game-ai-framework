namespace DecisionMaking.FiniteStateMachine;

public class RandomEdgeChain: EdgeChain
{
    private readonly Random _rng;
    public RandomEdgeChain(EdgeChain next) : base(next)
    { 
        _rng = new Random();
    }

    public override State ChooseNextState(List<Edge> edges)
    {
        return edges[_rng.Next(0, edges.Count)].Traverse();
    }
}