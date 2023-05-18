using DecisionMaking.BehaviorTree.Task.Leaf;
using Xunit.Abstractions;

namespace Framework.BehaviorTree.Test.Leafs; 

public class AttackLeaf: LeafLogic {
    private int _hitTime = 1000;
    public int HitsCount = 0;
    private bool _isHitting;

    public AttackLeaf(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    public override int Priority { get; } = 5;

    private ITestOutputHelper _testOutputHelper;
    
    public override void Start()
    {
        HitsCount = 0;
        _testOutputHelper.WriteLine($"Started attacking.");
    }
    public override async Task Update(float delta)
    {
        if (_isHitting)
            return;
        
        
        _isHitting = true;
        
        await Task.Delay(_hitTime);
        
        HitsCount++;
        _testOutputHelper.WriteLine($"Attacked {HitsCount} time(s).");
        _isHitting = false;
    }
}