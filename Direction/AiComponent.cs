using CoreEntities.Actions;
using CoreEntities.Blackboard;
using CoreEntities.DecisionMaker;
using Perception;

namespace Direction;

public class AiComponent {
    private readonly ActionManager _actionManager;
    private readonly DecisionMaker _decisionMaker;
    private readonly List<Sensor> _sensors;
    private readonly Blackboard _blackboard;

    public AiAction? CurrentAction => _actionManager.Active;
    
    public event Action<AiAction> ActionComplete;
    public event Action<AiAction> ActionSet;
    public event Action<AiAction> ActionInterrupted;

    public AiComponent(ActionManager actionManager, DecisionMaker decisionMaker, Blackboard bb, List<Sensor> sensors)
    {
        _actionManager = actionManager;
        _decisionMaker = decisionMaker;
        _blackboard = bb;
        _sensors = sensors;

        _actionManager.ActionComplete += (a) => ActionComplete?.Invoke(a);
        _actionManager.ActionSet += (a) => ActionSet?.Invoke(a);
        _actionManager.ActionInterrupted += (a) => ActionInterrupted?.Invoke(a);
        _actionManager.QueueEmpty += () =>
            _actionManager.ScheduleAction(decisionMaker.RequestAction());

        ActionComplete += decisionMaker.OnActionComplete;
        ActionSet += decisionMaker.OnActionSet;
        ActionInterrupted += decisionMaker.OnActionInterrupted;
    }

    private void UpdateSensors()
    {
        foreach (var sensor in _sensors)
        {
            sensor.Sense();
        }
    }

    public async Task Update(float delta)
    {
        _blackboard.Update(delta);
        UpdateSensors();

        var interruption = _decisionMaker.Update();

        if (interruption is not null && interruption.Interrupt == true)
            _actionManager.ScheduleAction(interruption);
        await _actionManager.Update(delta);
    }
}