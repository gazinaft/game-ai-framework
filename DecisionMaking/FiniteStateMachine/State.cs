namespace DecisionMaking.FiniteStateMachine;

public class State
{
    public event Action Processed;

    private StateLogic _logic;
    
    
    public List<Edge> Edges = new List<Edge>();

    public State(StateLogic logic)
    {
        _logic = logic;
    }

    public void Start(object actor)
    {
        _logic.Processed += () => Processed.Invoke();
        _logic.Actor = actor;
        _logic.Start();
    }

    public void Update(float delta)
    {
        _logic.Update(delta);
    }
}