namespace DecisionMaking.FiniteStateMachine.Transitions; 

public abstract class TransitionLogic {
    public abstract bool CanTraverse { get; }
}