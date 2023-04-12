using DecisionMaking.FiniteStateMachine.States;

namespace IntegrationTest;

public class RageLogic : StateLogic
{

    
    public override void Start()
    {
        Console.WriteLine("Starting rage");
    }

    public override void Update(float delta)
    {
        Console.WriteLine("raging");
    }
    
    
}
