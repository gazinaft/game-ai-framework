using System.Collections;

namespace CoreEntities.Blackboard;

public class Blackboard
{
    private Hashtable Entries = new();

    public void Set<T>(string id, BlackboardData<T> data)
    {
        Entries.Add(id, data);
    }
    
    public BlackboardData<T>? Get<T>(string id)
    {
        return Entries[id] as BlackboardData<T>;
    }
}