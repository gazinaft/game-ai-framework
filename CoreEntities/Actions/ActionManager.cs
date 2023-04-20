namespace CoreEntities.Actions;

public class ActionManager
{
    private readonly List<AiAction> _queue;
    private AiAction? _active;
    private int _priorityCutOff;

    public event Action? ActionComplete;

    public ActionManager()
    {
        _queue = new List<AiAction>();
        _active = null;
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
        _active!.Update(delta);

        if (_active.IsComplete)
        {
            ActionComplete?.Invoke();
            _active = null;
        }
    }

    private void TrimQueue(float delta)
    {
        foreach (var aiAction in _queue)
        {
            aiAction.ExpireTime -= delta;
        }
        _priorityCutOff = _active?.Priority ?? 0;

        _queue.RemoveAll(x => x.ExpireTime < 0);
        // highest(biggest) priority must be first
        _queue.Sort((x, y) => y.Priority - x.Priority);
    }
    
    private void ReconsiderActive()
    {
        if (_active is null)
        {
            SetActive(_queue[0]);
        }
        else
        {
            var actionToInterrupt = _queue.FirstOrDefault(a => a.Interrupt && a.Priority >= _priorityCutOff);
            if (actionToInterrupt is null || actionToInterrupt == _active) return;
            SetActive(actionToInterrupt);
        }
    }

    private void SetActive(AiAction action)
    {
        _queue.Remove(action);
        _active = action;
        _active.Start();
    }
}
