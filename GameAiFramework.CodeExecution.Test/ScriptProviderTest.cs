using DecisionMaking.FiniteStateMachine.States;
using Microsoft.Extensions.DependencyInjection;

namespace CodeExecution.Test;

public class ScriptProviderTest {
    private IServiceProvider _serviceProvider;
    
    private class StateLogicOption {
        public int Value { get; } = 5;
    }
    
    private class StateLogicTest : StateLogic {
        public StateLogicOption Option { get; }

        public StateLogicTest(StateLogicOption option)
        {
            Option = option;
        }
        public override void Start()
        {
            
        }
        public override async Task Update(float delta)
        {
            
        }
    }


    public ScriptProviderTest()
    {

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<StateLogicOption>();
        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    [Fact]
    public void TestReturningInstanceOfSpecifiedClass()
    {
        var scriptProvider = new ScriptProvider(_serviceProvider);
        var script = scriptProvider.GetClassByNameExtending<StateLogic>("StateLogicTest", GetType().Assembly);

        Assert.NotNull(script);
        Assert.Equal(5, ((StateLogicTest)script).Option.Value);
    }
}