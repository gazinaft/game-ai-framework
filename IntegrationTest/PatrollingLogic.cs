using DecisionMaking.FiniteStateMachine.States;

namespace IntegrationTest;

public class PatrollingLogic : StateLogic
{
    private readonly float _patrolTime;
    private float _currentPatrolTime;
    public PatrollingLogic(float patrolTime)
    {
        _patrolTime = patrolTime;
    }
    
    public override void Start()
    {
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