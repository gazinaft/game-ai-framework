using System.Reflection;
using CoreEntities.Actions;
using CoreEntities.Blackboard;
using CoreEntities.DecisionMaker;
using Perception;

namespace Direction; 

public class AiComponent {
    private readonly ActionManager _actionManager;
    private readonly DecisionMaker _decisionMaker;
    private List<Sensor> _sensors;
    private Blackboard _blackboard;

    public AiComponent(ActionManager actionManager, DecisionMaker decisionMaker, Blackboard bb, List<Sensor> sensors)
    {
        _actionManager = actionManager;
        _decisionMaker = decisionMaker;
        _blackboard = bb;
        _sensors = sensors;
        _actionManager.ScheduleAction(decisionMaker.GetNextAction());
        _actionManager.QueueEmpty += () => _actionManager.ScheduleAction(decisionMaker.GetNextAction());
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
        
        _actionManager.Update(delta);
        
        var interruption = _decisionMaker.SearchForInterruptions();

        if (interruption is not null)
        {
            _actionManager.ScheduleAction(interruption);
        }
    }
}
