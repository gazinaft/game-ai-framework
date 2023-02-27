namespace DecisionMaking.FiniteStateMachine;

public class State
{
    public Action processed;

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
        // TODO rewrite signal logic

        _logic.Actor = actor;
        _logic.Ready();
    }

    public void Process(float delta)
    {
        _logic.Process();
    }
}