namespace Framework.Test.StateLogics;

public class RageLogic : StateLogic {
    public override int Priority { get; } = 5;
    
    private float _ragetime;
    private ITestOutputHelper _testOutputHelper;
    
    public override void Start()
    {
        _ragetime = 0;
        _testOutputHelper.WriteLine("Starting rage");
    }

    public override async Task Update(float delta)
    {
        _ragetime += delta;
        _testOutputHelper.WriteLine("raging for " + _ragetime);
    }


    public RageLogic(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
}
