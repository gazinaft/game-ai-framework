using System.Runtime.CompilerServices;

namespace Framework.Test;

public static class Program
{
    // 1000 ms/60fps = 17ms timeout
    private const int Stable60FpsInterval = 17;

    public static ITestOutputHelper TestOutputHelper;
    public static void Run(string[] args)
    {
    
        var actor = new ActorStub(TestOutputHelper);
        actor.Start();
        for (int i = 0; i < 100; i++)
        {
            TestOutputHelper.WriteLine("i = " + i);
            if (i % 10 == 0)
            {
                actor.sensor.ActorStub = actor;
                TestOutputHelper.WriteLine("Set enemy");
            }
    
            if (i % 13 == 0)
            {
                actor.sensor.ActorStub = null;
                TestOutputHelper.WriteLine("enemy null");
            }
            actor.Update(Stable60FpsInterval);
            
        }
    }
}