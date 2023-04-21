namespace Framework.Test.StateLogics;

public class RageLogic : StateLogic
{
    private float _ragetime;
    private ITestOutputHelper _testOutputHelper;
    
    public override void Start()
    {
        _ragetime = 0;
        _testOutputHelper.WriteLine("Starting rage");
    }

    public override void Update(float delta)
    {
        _ragetime += delta;
        _testOutputHelper.WriteLine("raging for " + _ragetime);
    }


    public RageLogic(int priority, float expireTime, ITestOutputHelper testOutputHelper) : base(priority, expireTime)
    {
        _testOutputHelper = testOutputHelper;
    }
}
