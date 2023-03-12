namespace CoreEntities.Actions;

public abstract class AiAction
{
    public int Priority { get; }
    public long ExpireTime { get; }
    
    public abstract bool IsComplete();
    public abstract bool Interrupt();

    public abstract void Start();
    public abstract void Update();

}