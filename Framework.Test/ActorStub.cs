using CoreEntities.Actions;
using CoreEntities.Blackboard;
using DecisionMaking.FiniteStateMachine.StateMachine;
using DecisionMaking.FiniteStateMachine.States;
using DecisionMaking.FiniteStateMachine.Transitions;
using Direction;
using Framework.Test.StateLogics;
using Framework.Test.Transitions;
using Perception;

namespace Framework.Test;

public class ActorStub
{
    private AiComponent _aiComponent;
    
    // later will be only in AIComponent and update through sensors in the component
    public Blackboard GlobalBb;
    
    public void Start()
    {
        #region [ Ai Construction ]
        
        var bb = new Blackboard();

        var idleLogic = new IdleLogic(1, 20);
        var rageLogic = new RageLogic(5, 25);
        var patrollingLogic = new PatrollingLogic(25f, 5, 25);
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
        _aiComponent = new AiComponent(am, fsm, bb, new List<Sensor> { sensor });
        #endregion
    }
    
    public void Update(float delta)
    {
        _aiComponent.Update(delta);
    }
}