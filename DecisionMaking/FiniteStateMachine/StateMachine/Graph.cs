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

    public override AiAction RequestAction()
    {
        if (_currentState is null)
        {
            _currentState = _root;

            return _currentState.Logic;
        }
        
        var nextState = _currentState?.GetNextState();
     
        if (nextState?.Logic == null) 
            throw new NullReferenceException("Next State is null or has null logic");
        
        _currentState = nextState;
        return nextState.Logic;
    }

    public override AiAction? Update()
    {
        var nextState = _currentState?.GetInterruption();
        if (nextState?.Logic == null) return null;
        
        _currentState = nextState;
        nextState.Logic.Interrupt = true;
        return nextState.Logic;
    }
}