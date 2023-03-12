namespace CoreEntities.Actions;

public class ActionManager
{
    private readonly List<AiAction> _queue;
    private AiAction? _active;

    public ActionManager()
    {
        _queue = new List<AiAction>();
        _active = null;
    }

    public void ScheduleAction(AiAction action)
    {
        _queue.Add(action);
    }

    public void Process(float delta)
    {
        if (_queue.Count == 0) return;
        
        var currentTimeMs = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        var priorityCutoff = _active?.Priority ?? 0;

        _queue.RemoveAll(x => x.ExpireTime < currentTimeMs);
        _queue.Sort((x, y) => x.Priority - y.Priority);

        foreach (var action in _queue.ToArray())
        {
            if (action.Priority < priorityCutoff)
                break;

            if (action.Interrupt())
            {
                _queue.Remove(action);
                _active = action;
                priorityCutoff = action.Priority;
            }
        }

        if (_active == null || _active.IsComplete())
        {
            var newActive = _queue[0];
            _queue.RemoveAt(0);
            _active = newActive;
            _active.Start();
        }
        else
        {
            _active.Update(delta);
        }
    }
}