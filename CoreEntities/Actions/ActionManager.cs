namespace CoreEntities.Actions;

public class ActionManager
{
    private readonly List<AiAction> _queue;
    private AiAction? _active;
    private int _priorityCutOff;

    public event Action? QueueEmpty;

    public ActionManager()
    {
        _queue = new List<AiAction>();
        _active = null;
    }

    public void ScheduleAction(AiAction action)
    {
        _queue.Add(action);
    }

    public void Update(float delta)
    {
        if (_queue.Count == 0)
        {
            QueueEmpty?.Invoke();
            return;
        }
        
        // TrimQueue();

        ReconsiderActive();

        _active!.Update(delta);
        
        if (_active.IsComplete)
            _active = null;
    }

    private void TrimQueue()
    {
        var currentTimeMs = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        _priorityCutOff = _active?.Priority ?? 0;

        _queue.RemoveAll(x => x.ExpireTime < currentTimeMs);
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