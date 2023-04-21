namespace Framework.Test.StateLogics;

public class PatrollingLogic : StateLogic
{
    private readonly float _patrolTime;
    private float _currentPatrolTime;
    private ITestOutputHelper _testOutputHelper;
    public PatrollingLogic(float patrolTime, int priority, float expireTime, ITestOutputHelper testOutputHelper) : base(priority, expireTime)
    {
        _patrolTime = patrolTime;
        _testOutputHelper = testOutputHelper;
    }
    
    public override void Start()
    {
        IsComplete = false;
        _currentPatrolTime = 0;
        _testOutputHelper.WriteLine("Starting Patrol");
    }

    public override void Update(float delta)
    {
        _currentPatrolTime += delta;
        _testOutputHelper.WriteLine("Patrolling for " + _currentPatrolTime + " of " + _patrolTime);
        if (_currentPatrolTime >= _patrolTime)
        {
            IsComplete = true;
        }
        
    }
}