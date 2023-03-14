using CoreEntities.Actions;
using CoreEntities.DecisionMaker;

namespace Direction; 

public class AiDirector {
    private readonly ActionManager _actionManager;
    private readonly DecisionMaker _decisionMaker;

    public AiDirector(ActionManager actionManager, DecisionMaker decisionMaker)
    {
        _actionManager = actionManager;
        _decisionMaker = decisionMaker;
        
        _actionManager.QueueEmpty += () => _actionManager.ScheduleAction(decisionMaker.GetNextAction());
    }

    public void Direct(float delta)
    {
        _actionManager.Update(delta);
        
        var interruption = _decisionMaker.SearchForInterruptions();

        if (interruption is not null)
        {
            _actionManager.ScheduleAction(interruption);
        }
    }
}