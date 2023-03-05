namespace DecisionMaking.FiniteStateMachine;

public class State
{
    public event Action Processed;

    private Executable _executable;
    private StateLogic _logic;
    
    
    public List<Edge> Edges = new List<Edge>();

    public State(Executable executable)
    {
        _executable = executable;
    }

    public void Start(object actor)
    {
        _logic = _executable.Logic;
        _logic.Processed += () => Processed.Invoke();
        _logic.Actor = actor;
        _logic.Ready();
    }

    public void Process(float delta)
    {
        _logic.Process();
    }
}