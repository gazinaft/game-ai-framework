using CoreEntities.Blackboard;
using DecisionMaking.FiniteStateMachine.States;
using DecisionMaking.FiniteStateMachine.Transitions;

namespace IntegrationTest;

public class FromRageLogic : TransitionLogic
{
    private Blackboard _bb;
    public FromRageLogic(Blackboard bb)
    {
        _bb = bb;
    }

    public override bool CanTraverse => _bb.Get<ActorStub>("Enemy")?.Value == null;
    
}