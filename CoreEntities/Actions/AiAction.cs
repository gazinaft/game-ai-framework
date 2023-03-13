namespace CoreEntities.Actions;

public abstract class AiAction
{
    public int Priority { get; }
    public long ExpireTime { get; }
    
    public bool IsComplete { get; }
    public bool Interrupt { get; }

    public abstract void Start();
    public abstract void Update(float delta);

}