namespace CoreEntities.Actions;

public abstract class AiAction
{
    public const float DefaultExpireTime = 5000;
    public const int DefaultPriority = 0;
    
    public virtual int Priority { get; } = DefaultPriority;
    protected virtual float StartExpireTime { get; } = DefaultExpireTime;
    public float ExpireTime { get; set; }

    public void QueueUp()
    {
        ExpireTime = StartExpireTime;
    }
    
    public virtual bool IsComplete { get; protected set; }
    public bool Interrupt { get; set; }

    public abstract void Start();
    public abstract Task Update(float delta);

}