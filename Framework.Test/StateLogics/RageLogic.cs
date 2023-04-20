using DecisionMaking.FiniteStateMachine.States;

namespace Framework.Test.StateLogics;

public class RageLogic : StateLogic
{
    private float _ragetime;
    
    public override void Start()
    {
        _ragetime = 0;
        Console.WriteLine("Starting rage");
    }

    public override void Update(float delta)
    {
        _ragetime += delta;
        Console.WriteLine("raging for " + _ragetime);
    }


    public RageLogic(int priority, long expireTime) : base(priority, expireTime)
    {
    }
}
