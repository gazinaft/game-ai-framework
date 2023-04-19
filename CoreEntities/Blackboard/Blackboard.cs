using System.Collections;

namespace CoreEntities.Blackboard;

public class Blackboard
{
    private Dictionary<string, BlackboardData<object?>> _entries = new();
    public void Set(string id, object? data)
    {
        _entries[id] = new BlackboardData<object?> { Value = data };
    }

    public BlackboardData<object?> GetUntyped(string id)
    {
        return _entries[id];
    }

    public BlackboardData<T>? Get<T>(string id) where T : class
    {
        if (!_entries.ContainsKey(id)) return null;
        var bbData = _entries[id];
        return new BlackboardData<T>
        {
            Key = bbData.Key,
            Timestamp = bbData.Timestamp,
            Value = (bbData.Value as T)!
        };
    }
}