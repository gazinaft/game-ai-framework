using DecisionMaking.FiniteStateMachine.States;

namespace IntegrationTest;

public class IdleLogic: StateLogic
{
    private float idleTime;
    public override void Start()
    {
        idleTime = 0;
    }

    public override void Update(float delta)
    {
        idleTime += delta;
        Console.WriteLine("Idling for " + idleTime + "ms");
    }
}