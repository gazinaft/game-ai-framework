using CoreEntities.Actions;
using CoreEntities.DecisionMaker;
using DecisionMaking.FiniteStateMachine.Exceptions;
using DecisionMaking.FiniteStateMachine.States;

namespace DecisionMaking.FiniteStateMachine.StateMachine;

public class Graph : DecisionMaker {
    private readonly State _root;
    private State? _currentState;

    public Graph(State root)
    {
        _root = root;
    }

    public override List<AiAction> OnActionComplete()
    {
        var nextState = _currentState?.GetNextState();
     
        if (nextState?.Logic == null) return new List<AiAction>();
        
        _currentState = nextState;
        return new List<AiAction> { nextState.Logic };
    }

    public override List<AiAction> Update(float delta)
    {
        if (_currentState is null)
        {
            _currentState = _root;

            return new List<AiAction> { _currentState.Logic };
        }
        
        var nextState = _currentState.GetInterruption();
        if (nextState?.Logic == null) return new List<AiAction>();
        
        _currentState = nextState;
        nextState.Logic.SetInterrupted();
        return new List<AiAction> { nextState.Logic  };
    }
}