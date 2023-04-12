using CoreEntities.Blackboard;

namespace IntegrationTest;

static class Program
{
    // 1000 ms/60fps = 17ms timeout
    private const int Stable60FpsInterval = 17;
    
    static void Main(string[] args)
    {

        var actor = new ActorStub();
        var globalBb = new Blackboard();

        actor.GlobalBb = globalBb;
        actor.Start();
        for (int i = 0; i < 10000; i++)
        {
            actor.Update(Stable60FpsInterval);
            if (i % 500 == 0)
            {
                globalBb.Set("enemy", new ActorStub());
            }

            if (i % 520 == 0)
            {
                globalBb.Set("enemy", null);
            }
        }
    }
}