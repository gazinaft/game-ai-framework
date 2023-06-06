using DecisionMaking.FiniteStateMachine.States;

namespace DecisionMaking.FiniteStateMachine.Transitions;

public class TransitionAfterComplete : Transition
{
    public TransitionAfterComplete(State to) : base(to, new AlwaysTransitionLogic(), false)
    {
    }
}