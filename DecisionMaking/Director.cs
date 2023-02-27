using DecisionMaking.FiniteStateMachine;

namespace DecisionMaking;

public class Director
{
    private EdgeChain _edgeChain;

    public State root;
    public State currentState;
    public Object actor;

    public Director(State graph)
    {
        this.root = graph;
    }

    public void SwitchState()
    {
        State nextState;
        if (currentState == null)
        {
            nextState = root;
        }
        else
        {
            nextState = _edgeChain.ChooseNextState(currentState.Edges);
        }
        //TODO rewrite signal logic

        currentState = nextState;
        currentState.Start(actor);
    }
}