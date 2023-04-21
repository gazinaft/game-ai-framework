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
    
    // later will be only in AIComponent and update through sensors in the component
    public Blackboard GlobalBb;
    
    public void Start()
    {
        #region [ Ai Construction ]
        
        var bb = new Blackboard();

        var idleLogic = new IdleLogic(1, _expireTime, _testOutputHelper);
        var rageLogic = new RageLogic(5, _expireTime, _testOutputHelper);
        var patrollingLogic = new PatrollingLogic(_patrolTime, 5, _expireTime, _testOutputHelper);
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

        var sensor = new SampleSensor(typeof(ActorStub), "Enemy", GlobalBb);
        
        var fsm = new Graph(idleState);
        var am = new ActionManager();
        AiComponent = new AiComponent(am, fsm, bb, new List<Sensor> { sensor });
        #endregion
    }
    
    public void Update(float delta)
    {
        AiComponent.Update(delta);
    }
}