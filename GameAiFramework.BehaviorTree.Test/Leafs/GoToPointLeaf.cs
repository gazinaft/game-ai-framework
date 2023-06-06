using DecisionMaking.BehaviorTree.Task.Leaf;
using System.Numerics;
using Xunit.Abstractions;

namespace Framework.BehaviorTree.Test.Leafs;

public class GoToPointLeaf : LeafLogic {
    public static Vector2 PointA { get; } = new Vector2(0, 0);
    public static Vector2 PointB { get; } = new Vector2(1, 0);
    public static Vector2 PointC { get; } = new Vector2(0, 1);

    public char Point { get; }
    private int _walkingTIme;
    private bool _isActive;

    private ITestOutputHelper _testOutputHelper;

    public GoToPointLeaf(char point, int walkingTIme, ITestOutputHelper testOutputHelper)
    {
        Point = point;
        _testOutputHelper = testOutputHelper;
        _walkingTIme = walkingTIme;
    }

    public override void Start()
    {
        _testOutputHelper.WriteLine($"Started walking to point: {Point}.");
    }
    public override async Task Update(float delta)
    {
        await Task.Delay(_walkingTIme);

        _testOutputHelper.WriteLine($"Finished walking to point: {Point}.");
        IsComplete = true;
    }
}