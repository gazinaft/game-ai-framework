using System.Collections;

namespace CoreEntities.Blackboard;

public class Blackboard
{
    private Dictionary<string, BlackboardData<object>?> _entries = new();

    public void Set<T>(string id, BlackboardData<T> data)
    {
        _entries.Add(id, data as BlackboardData<object>);
    }
    
    public BlackboardData<T>? Get<T>(string id)
    {
        if (!_entries.ContainsKey(id)) return null;
        return _entries[id] as BlackboardData<T>;
    }
}