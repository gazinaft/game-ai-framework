namespace CoreEntities.Actions;

public class ActionManager
{
    private readonly List<AiAction> _queue;
    private AiAction? _active;
    private int _priorityCutOff;

    public event Action? QueueEmpty;
    public event Action<AiAction>? ActionComplete;

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
        
        if (_queue.Count == 0)
        {
            QueueEmpty?.Invoke();
        }
        
        if (_active.IsComplete)
        {
            ActionComplete?.Invoke(_active);
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
        _queue.Sort((x, y) => x.Priority - y.Priority);
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
            if (actionToInterrupt == _active) return;
            if (actionToInterrupt is not null)
            {
                SetActive(actionToInterrupt);
            }
        }
    }

    private void SetActive(AiAction action)
    {
        _queue.Remove(action);
        _active = action;
        _active.Start();
    }
}
