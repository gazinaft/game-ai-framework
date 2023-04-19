using CoreEntities.Blackboard;
using DecisionMaking.FiniteStateMachine.Transitions;

namespace IntegrationTest;

public class ToRageLogic : TransitionLogic
{
    private Blackboard _bb;

    public override bool CanTraverse => _bb.Get<ActorStub>("Enemy")?.Value != null;

    public ToRageLogic(Blackboard bb)
    {
        _bb = bb;
    }
}