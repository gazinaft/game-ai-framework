using System.Reflection;
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

    public AiComponent(ActionManager actionManager, DecisionMaker decisionMaker, Blackboard bb, List<Sensor> sensors)
    {
        _actionManager = actionManager;
        _decisionMaker = decisionMaker;
        _blackboard = bb;
        _sensors = sensors;
        
        _actionManager.ActionComplete += action =>
        {
                List<AiAction> actions = decisionMaker
                    .OnActionComplete(action)
                    .Where(x => x != null) as List<AiAction> ?? new List<AiAction>();
                _actionManager.ScheduleActions(actions);
        };
    }

    private void UpdateSensors()
    {
        foreach (var sensor in _sensors)
        {
            _blackboard.Set(sensor.Id,sensor.Sense());       
        }
    }
    
    public void Update(float delta)
    {
        UpdateSensors();
        
        var interruptions = _decisionMaker.Update(delta);

        _actionManager.ScheduleActions(interruptions);
        
        _actionManager.Update(delta);
        
    }
}
