namespace CoreEntities.Actions;

public abstract class AiAction
{
    public int Priority { get; protected set;}
    public long ExpireTime { get; protected set;}
    
    public bool IsComplete { get; protected set; }
    public bool Interrupt { get; protected set; }

    public abstract void Start();
    public abstract void Update(float delta);

}