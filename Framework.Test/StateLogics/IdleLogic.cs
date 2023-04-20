using DecisionMaking.FiniteStateMachine.States;

namespace Framework.Test.StateLogics;

public class IdleLogic: StateLogic
{
    private float _idleTime;
    public override void Start()
    {
        _idleTime = 0;
        Console.WriteLine("Starting idle");
    }

    public override void Update(float delta)
    {
        _idleTime += delta;
        Console.WriteLine("Idling for " + _idleTime + " ms");
    }

    public IdleLogic(int priority, long expireTime) : base(priority, expireTime)
    {
    }
}