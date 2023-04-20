using System.Collections;

namespace CoreEntities.Blackboard;

public class Blackboard
{
    private readonly Dictionary<string, BlackboardData<object?>> _entries = new();
    public void Set(string id, object? data)
    {
        _entries[id] = new BlackboardData<object?> { Value = data, Timestamp = 0 };
    }

    public void Update(float delta)
    {
        foreach (var entry in _entries)
        {
            entry.Value.Timestamp += delta;
        }
    }

    public BlackboardData<T?> Get<T>(string id) where T : class
    {
        if (!_entries.ContainsKey(id)) return new BlackboardData<T?>
        {
            Key = id,
            Timestamp = 0,
            Value = null
        };
        
        var bbData = _entries[id];
        return new BlackboardData<T?>
        {
            Key = bbData.Key,
            Timestamp = bbData.Timestamp,
            Value = bbData.Value as T
        };
    }
}