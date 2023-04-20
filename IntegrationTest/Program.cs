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
        for (int i = 0; i < 100; i++)
        {
            Console.Write("i = " + i + " | ");
            if (i % 10 == 0)
            {
                globalBb.Set("Enemy", new ActorStub());
                Console.Write("Set enemy | ");
            }

            if (i % 13 == 0)
            {
                globalBb.Set("Enemy", null);
                Console.Write("enemy null | ");
            }
            actor.Update(Stable60FpsInterval);
            
        }
    }
}