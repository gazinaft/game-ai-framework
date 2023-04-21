namespace Framework.Test.Transitions;

public class FromRageLogic : TransitionLogic
{
    private Blackboard _bb;
    public FromRageLogic(Blackboard bb)
    {
        _bb = bb;
    }

    public override bool CanTraverse => _bb.Get<ActorStub>("Enemy")?.Value == null;
    
}