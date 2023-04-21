namespace CoreEntities.Actions;

public class ActionManager
{
    private readonly List<AiAction> _queue;
    public AiAction? Active { get; private set; }
    private int _priorityCutOff;

    public event Action? ActionComplete;

    public ActionManager()
    {
        _queue = new List<AiAction>();
        Active = null;
    }

    public void ScheduleActions(List<AiAction> actions)
    {
        foreach (var action in actions)
        {
            _queue.Add(action);
            action.QueueUp();
        }
    }

    public void Update(float delta)
    {
        TrimQueue(delta);
        ReconsiderActive();
        Active!.Update(delta);

        if (Active.IsComplete)
        {
            ActionComplete?.Invoke();
            Active = null;
        }
    }

    private void TrimQueue(float delta)
    {
        foreach (var aiAction in _queue)
        {
            aiAction.ExpireTime -= delta;
        }
        _priorityCutOff = Active?.Priority ?? 0;

        _queue.RemoveAll(x => x.ExpireTime < 0);
        // highest(biggest) priority must be first
        _queue.Sort((x, y) => y.Priority - x.Priority);
    }
    
    private void ReconsiderActive()
    {
        if (Active is null)
        {
            SetActive(_queue[0]);
        }
        else
        {
            var actionToInterrupt = _queue.FirstOrDefault(a => a.Interrupt && a.Priority >= _priorityCutOff);
            if (actionToInterrupt is null || actionToInterrupt == Active) return;
            SetActive(actionToInterrupt);
        }
    }

    private void SetActive(AiAction action)
    {
        _queue.Remove(action);
        Active = action;
        Active.Start();
    }
}
