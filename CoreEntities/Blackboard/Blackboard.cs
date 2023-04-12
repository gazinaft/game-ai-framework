using System.Collections;

namespace CoreEntities.Blackboard;

public class Blackboard
{
    private Dictionary<string, BlackboardData<object?>> _entries = new();
    public void Set(string id, object? data)
    {
        _entries[id] = new BlackboardData<object?> { Value = data };
    }
    
    public BlackboardData<T>? Get<T>(string id)
    {
        if (!_entries.ContainsKey(id)) return null;
        return _entries[id] as BlackboardData<T>;
    }
}