namespace Perception; 

public abstract class Sensor {
    protected Sensor(Type type, string id)
    {
        Type = type;
        Id = id;
    }

    public Type Type { get; }
    public string Id { get; }
    public abstract object? Sense();
}
