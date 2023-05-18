namespace CoreEntities.Blackboard;

public class BlackboardData<T>
{
    public string Key { get; set; }
    public T? Value { get; set; }
    public float Timestamp { get; set; }
}