namespace Framework.Test;

public class ActorStub
{
    public AiComponent AiComponent;

    private readonly float _patrolTime;
    private readonly float _expireTime;
    private ITestOutputHelper? _testOutputHelper;

    public ActorStub(ITestOutputHelper testOutputHelper = null, float patrolTime = 25f, float expireTime = 20f)
    {
        _testOutputHelper = testOutputHelper;
        _patrolTime = patrolTime;
        _expireTime = expireTime;
    }

    public void Start()
    {
        #region [ Ai Construction ]
        
        var bb = new Blackboard();

        var idleLogic = new IdleLogic(_testOutputHelper);
        var rageLogic = new RageLogic(_testOutputHelper);
        var patrollingLogic = new PatrollingLogic(_patrolTime, _testOutputHelper);
        var toRageLogic = new ToRageLogic(bb);
        var fromRageLogic = new FromRageLogic(bb);

        var idleState = new State(idleLogic);
        var rageState = new State(rageLogic);
        var patrollingState = new State(patrollingLogic);

        var transitionToRage = new Transition(rageState, toRageLogic, true);
        var transitionFromRageToPatrol = new Transition(patrollingState, fromRageLogic, true);
        var transitionFromPatrolToIdle = new TransitionAfterComplete(idleState);

        idleState.AddTransition(transitionToRage);
        patrollingState.AddTransition(transitionToRage).AddTransition(transitionFromPatrolToIdle);
        rageState.AddTransition(transitionFromRageToPatrol);

        sensor = new SampleSensor(bb, _testOutputHelper);

        var fsm = new Graph(idleState);
        var am = new ActionManager();
        AiComponent = new AiComponent(am, fsm, bb, new List<Sensor> { sensor });
        #endregion
    }
    public SampleSensor sensor { get; set; }

    public void Update(float delta)
    {
        AiComponent.Update(delta);
    }
}