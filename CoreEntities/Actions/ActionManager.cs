namespace CoreEntities.Actions;

public class ActionManager {
    private readonly List<AiAction> _queue;
    public AiAction? Active { get; private set; }
    private int _priorityCutOff;

    public event Action QueueEmpty;
    public event Action<AiAction> ActionComplete;
    public event Action<AiAction> ActionSet;
    public event Action<AiAction> ActionInterrupted;

    public ActionManager()
    {
        _queue = new List<AiAction>();
        Active = null;
    }

    public void ScheduleAction(AiAction action)
    {
        _queue.Add(action);
        action.QueueUp();
    }

    public async Task Update(float delta)
    {
        if (_queue.Count == 0 && Active == null)
            QueueEmpty?.Invoke();
        
        TrimQueue(delta);
        ReconsiderActive();
        await Active!.Update(delta);
        
        if (Active.IsComplete)
        {
            ActionComplete?.Invoke(Active);
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
            var actionToInterrupt = _queue.FirstOrDefault(a => a.Interrupt);
            if (actionToInterrupt is null || actionToInterrupt == Active) return;
            
            ActionInterrupted?.Invoke(actionToInterrupt);
            SetActive(actionToInterrupt);
        }
    }

    private void SetActive(AiAction action)
    {
        _queue.Remove(action);
        Active = action;
        Active.Start();
        ActionSet?.Invoke(Active);
    }
}