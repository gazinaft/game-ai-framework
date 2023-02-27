namespace DecisionMaking.FiniteStateMachine;

public class Executable
{
    private string _path;

    public StateLogic Logic
    {
        get
        {
            //TODO rewrite to fit C#
            return new StateLogic();
        }
    }
    
    
    public Executable(string path)
    {
        _path = path;
    }

}