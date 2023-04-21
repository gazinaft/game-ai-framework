using Xunit.Abstractions;

namespace Framework.Test.StateLogics;

public class IdleLogic: StateLogic
{
    private float _idleTime;
    private ITestOutputHelper _testOutputHelper;
    public override void Start()
    {
        _idleTime = 0;
        _testOutputHelper.WriteLine("Starting idle");
    }

    public override void Update(float delta)
    {
        _idleTime += delta;
        _testOutputHelper.WriteLine("Idling for " + _idleTime + " ms");
    }

    public IdleLogic(int priority, float expireTime, ITestOutputHelper testOutputHelper) : base(priority, expireTime)
    {
        _testOutputHelper = testOutputHelper;
    }
}