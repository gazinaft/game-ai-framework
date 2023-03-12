using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CodeExecution;

public class ScriptProvider {
    private readonly IServiceProvider _serviceProvider;
    
    public ScriptProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

    }

    public T GetClassByNameExtending<T>(string name, Assembly assembly)
    {
        var typeToCreate = assembly.DefinedTypes
            .Where(t => t.BaseType == typeof(T))
            .First(t => t.Name == name);

        return (T)ActivatorUtilities.CreateInstance(_serviceProvider, typeToCreate);
    }
}