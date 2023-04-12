using CoreEntities.Blackboard;
using DecisionMaking.FiniteStateMachine.States;
using DecisionMaking.FiniteStateMachine.Transitions;

namespace IntegrationTest;

public class FromRageLogic : TransitionLogic
{
    private Blackboard bb;
    public FromRageLogic(Blackboard bb)
    {
        this.bb = bb;
    }

    public override bool CanTraverse => bb.Get<ActorStub>("Enemy") == null;
    
}