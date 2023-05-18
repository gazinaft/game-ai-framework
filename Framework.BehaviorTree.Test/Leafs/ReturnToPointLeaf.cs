using Xunit.Abstractions;

namespace Framework.BehaviorTree.Test.Leafs; 

public class ReturnToPointLeaf: GoToPointLeaf {

    public ReturnToPointLeaf(char point, int walkingTIme, ITestOutputHelper testOutputHelper) : base(point, walkingTIme, testOutputHelper)
    {
    }
}