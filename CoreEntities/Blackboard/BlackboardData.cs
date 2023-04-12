namespace CoreEntities.Blackboard;

public class BlackboardData<T>
{
    public string Key { get; set; }
    public T Value { get; set; }
    public long Timestamp { get; set; }
}