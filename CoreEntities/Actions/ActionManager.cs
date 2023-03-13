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
        var currentTimeMs = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        var priorityCutoff = _active?.Priority ?? 0;

        _queue.RemoveAll(x => x.ExpireTime < currentTimeMs);
        _queue.Sort((x, y) => x.Priority - y.Priority);

        var actionToInterrupt = _queue.FirstOrDefault(a => a.Interrupt && a.Priority >= priorityCutoff);
        if (actionToInterrupt is not null) 
        {
            _queue.Remove(actionToInterrupt);
            _active = actionToInterrupt;
            _active.Start();
        }

        if (_active == null || _active.IsComplete)
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