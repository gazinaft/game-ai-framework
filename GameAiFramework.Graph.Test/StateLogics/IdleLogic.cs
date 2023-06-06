using Xunit.Abstractions;

namespace Framework.Test.StateLogics;

public class IdleLogic: StateLogic {
    public override int Priority { get; } = 1;
    
    private float _idleTime;
    private ITestOutputHelper _testOutputHelper;
    public override void Start()
    {
        _idleTime = 0;
        _testOutputHelper.WriteLine("Starting idle");
    }

    public override async Task Update(float delta)
    {
        _idleTime += delta;
        _testOutputHelper.WriteLine("Idling for " + _idleTime + " ms");
    }

    public IdleLogic(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
}