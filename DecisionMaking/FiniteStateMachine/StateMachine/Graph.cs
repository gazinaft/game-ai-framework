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

    public override AiAction GetNextAction()
    {
        if (_currentState is null)
        {
            _currentState = _root;

            return _currentState.Logic;
        }

        var nextState = _currentState.GetNextState();
        _currentState = nextState ?? throw new NextStateNotFoundException();

        return _currentState.Logic;
    }

    public override AiAction? SearchForInterruptions()
    {
        var nextState = _currentState?.GetInterruption();
        _currentState = nextState ?? _currentState;

        nextState?.Logic.SetInterrupted();
        return nextState?.Logic;
    }
}