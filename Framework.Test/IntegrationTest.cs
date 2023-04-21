
namespace Framework.Test;

public class IntegrationTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    // 1000 ms/60fps = 17ms timeout
    private const float Stable60FpsInterval = 17;
    private const float PatrolTime = 25f;
    private const float ExpireTime = 20f;
    
    private ActorStub actor;
    private Blackboard globalBb;

    public IntegrationTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    private void Init()
    {
        actor = new ActorStub(_testOutputHelper, PatrolTime, ExpireTime);
        globalBb = new Blackboard();

        actor.GlobalBb = globalBb;
        actor.Start();
    }

    [Fact]
    public void FromIdleToRage()
    {
        Init();
        for (int i = 0; i < 100; i++)
        {
            actor.Update(Stable60FpsInterval);
            Assert.IsType<IdleLogic>(actor.AiComponent.CurrentAction);
        }

        globalBb.Set("Enemy", new ActorStub());
        
        for (int i = 0; i < 100; i++)
        {
            actor.Update(Stable60FpsInterval);
            Assert.IsType<RageLogic>(actor.AiComponent.CurrentAction);
        }
    }

    [Fact]
    public void FromRageToPatrol()
    {
        Init();
        actor.Update(Stable60FpsInterval);
        Assert.IsType<IdleLogic>(actor.AiComponent.CurrentAction);
        
        globalBb.Set("Enemy", new ActorStub());
        for (int i = 0; i < 100; i++)
        {
            actor.Update(Stable60FpsInterval);
            Assert.IsType<RageLogic>(actor.AiComponent.CurrentAction);
        }
        globalBb.Set("Enemy", null);
        
        for (float i = 0; i < PatrolTime - Stable60FpsInterval; i += Stable60FpsInterval)
        {
            actor.Update(Stable60FpsInterval);
            Assert.IsType<PatrollingLogic>(actor.AiComponent.CurrentAction);
        }
        actor.Update(Stable60FpsInterval);
        // Patrol is complete and state is cleared
        Assert.Null(actor.AiComponent.CurrentAction);
    }

    [Fact]
    public void FromPatrolToIdle()
    {
        Init();
        actor.Update(Stable60FpsInterval);
        Assert.IsType<IdleLogic>(actor.AiComponent.CurrentAction);
        
        globalBb.Set("Enemy", new ActorStub());
        actor.Update(Stable60FpsInterval);
        Assert.IsType<RageLogic>(actor.AiComponent.CurrentAction);
        
        globalBb.Set("Enemy", null);
        
        for (float i = 0; i < PatrolTime - Stable60FpsInterval; i += Stable60FpsInterval)
        {
            actor.Update(Stable60FpsInterval);
            Assert.IsType<PatrollingLogic>(actor.AiComponent.CurrentAction);
        }
        
        actor.Update(Stable60FpsInterval);
        // Patrol is complete and state is cleared
        Assert.Null(actor.AiComponent.CurrentAction);

        for (int i = 0; i < 100; i++)
        {
            actor.Update(Stable60FpsInterval);
            Assert.IsType<IdleLogic>(actor.AiComponent.CurrentAction);
        }
    }

    [Fact]
    public void FromPatrolToRage()
    {
        Init();
        actor.Update(Stable60FpsInterval);
        Assert.IsType<IdleLogic>(actor.AiComponent.CurrentAction);
        
        globalBb.Set("Enemy", new ActorStub());
        actor.Update(Stable60FpsInterval);
        Assert.IsType<RageLogic>(actor.AiComponent.CurrentAction);
        
        globalBb.Set("Enemy", null);
        
        for (float i = 0; i < PatrolTime - Stable60FpsInterval; i += Stable60FpsInterval)
        {
            actor.Update(Stable60FpsInterval);
            Assert.IsType<PatrollingLogic>(actor.AiComponent.CurrentAction);
        }
        globalBb.Set("Enemy", new ActorStub());
        
        // Patrol is interrupted and back to rage
        for (int i = 0; i < 100; i++)
        {
            actor.Update(Stable60FpsInterval);
            Assert.IsType<RageLogic>(actor.AiComponent.CurrentAction);
        }
    }

    [Fact]
    private void ConsoleRun()
    {
        Program.TestOutputHelper = _testOutputHelper;
        Program.Run(new []{""});
    }
}