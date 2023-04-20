using DecisionMaking.FiniteStateMachine.States;

namespace Framework.Test.StateLogics;

public class PatrollingLogic : StateLogic
{
    private readonly float _patrolTime;
    private float _currentPatrolTime;
    public PatrollingLogic(float patrolTime, int priority, long expireTime) : base(priority, expireTime)
    {
        _patrolTime = patrolTime;
    }
    
    public override void Start()
    {
        IsComplete = false;
        _currentPatrolTime = 0;
    }

    public override void Update(float delta)
    {
        _currentPatrolTime += delta;
        Console.WriteLine("Patrolling for " + _currentPatrolTime + " of " + _patrolTime);
        if (_currentPatrolTime >= _patrolTime)
        {
            IsComplete = true;
        }
        
    }
}