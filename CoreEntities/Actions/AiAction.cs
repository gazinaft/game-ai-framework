namespace CoreEntities.Actions;

public abstract class AiAction
{
    public int Priority { get; protected set;}
    protected float StartExpireTime { get; }
    public float ExpireTime { get; set; } 

    protected AiAction(int priority, float expireTime)
    {
        Priority = priority;
        StartExpireTime = expireTime;
    }

    public void QueueUp()
    {
        ExpireTime = StartExpireTime;
    }
    
    public bool IsComplete { get; protected set; }
    public bool Interrupt { get; protected set; }

    public abstract void Start();
    public abstract void Update(float delta);

}