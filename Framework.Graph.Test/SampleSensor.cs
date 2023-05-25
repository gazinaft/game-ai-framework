namespace Framework.Test;

public class SampleSensor : Sensor {
    public ActorStub? ActorStub { get; set; }
    private ITestOutputHelper _testOutputHelper;
    public SampleSensor(Blackboard global, ITestOutputHelper testOutputHelper): base(global)
    {
        _testOutputHelper = testOutputHelper;
    }

    public override void Sense(float delta)
    {
        _testOutputHelper.WriteLine(ActorStub?.ToString() ?? "null");
        
        _blackboard.Set("Enemy", ActorStub);
        _testOutputHelper.WriteLine((_blackboard.Get<ActorStub>("Enemy").Value != null).ToString());
    }
}