namespace Framework.Test.StateLogics;

public class PatrollingLogic : StateLogic {
    public override int Priority { get; } = 5;
    
    private readonly float _patrolTime;
    private float _currentPatrolTime;
    private ITestOutputHelper _testOutputHelper;
    public PatrollingLogic(float patrolTime, ITestOutputHelper testOutputHelper)
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

    public override async Task Update(float delta)
    {
        _currentPatrolTime += delta;
        _testOutputHelper.WriteLine("Patrolling for " + _currentPatrolTime + " of " + _patrolTime);
        if (_currentPatrolTime >= _patrolTime)
        {
            IsComplete = true;
        }
        
    }
}