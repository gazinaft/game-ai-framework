using CoreEntities.Actions;
using CoreEntities.Blackboard;
using DecisionMaking.BehaviorTree.Task;
using DecisionMaking.BehaviorTree.Task.Decorator;
using DecisionMaking.BehaviorTree.Task.Leaf;
using DecisionMaking.BehaviorTree.Task.Selector;
using DecisionMaking.BehaviorTree.Task.Sequence;
using Direction;
using Framework.BehaviorTree.Test.Decorators;
using Framework.BehaviorTree.Test.Leafs;
using Framework.BehaviorTree.Test.Sensors;
using Perception;
using System.Numerics;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Framework.BehaviorTree.Test;

public class IntegrationTest {
    // 1000 ms/60fps = 17ms timeout
    private const float Stable60FpsInterval = 17;

    private readonly AiComponent _ai;
    private readonly EnemyInRangeSensor _enemyInRangeSensor;
    private readonly AiPositionSensor _aiPositionSensor;
    private ITestOutputHelper _testOutput;

    public IntegrationTest(ITestOutputHelper testOutput)
    {
        _testOutput = testOutput;

        var actionManager = new ActionManager();
        var tree = new DecisionMaking.BehaviorTree.BehaviorTree();
        var bb = new Blackboard();

        var dec1 = new Decorator(new EnemyInRangeDecorator(bb), new Leaf(new AttackLeaf(_testOutput)));

        var goToA = new GoToPointLeaf('A', 200, _testOutput);
        var goToB = new GoToPointLeaf('B', 100, _testOutput);
        var goToC = new GoToPointLeaf('C', 300, _testOutput);

        var seq = new Sequence(new List<TreeTask> {new Leaf(goToB), new Leaf(goToC), new Leaf(goToA)}, tree);
        var dec2 = new Decorator(new OnPointADecorator(bb), seq);
        var sel1 = new Selector(new List<TreeTask> {dec2, new Leaf(new ReturnToPointLeaf('A', 400, _testOutput))});

        var sel2 = new Selector(new List<TreeTask> {dec1, sel1});

        tree.Root = sel2;

        _enemyInRangeSensor = new EnemyInRangeSensor(bb);
        _aiPositionSensor = new AiPositionSensor(bb);
        _ai = new AiComponent(actionManager, tree, bb, new List<Sensor>
        {
            _enemyInRangeSensor,
            _aiPositionSensor
        });
    }

    [Fact]
    public void TestInfinitePatrolSequence()
    {
        var dict = new Dictionary<char, int>
        {
            {'A', 0},
            {'B', 0},
            {'C', 0},
        };

        _ai.ActionComplete += action => { dict[((GoToPointLeaf) action).Point] += 1; };

        while (dict.Values.Sum() < 9)
        {
            var task = _ai.Update(Stable60FpsInterval);
            task.Wait();
        }

        Assert.Equal(3, dict['A']);
        Assert.Equal(3, dict['B']);
        Assert.Equal(3, dict['C']);
    }

    [Fact]
    public void TestAttackDuringPatrol()
    {
        _ai.ActionComplete += action => {
            Assert.IsType<GoToPointLeaf>(action);
        };

        for (var i = 0; i < 2; i++)
        {
            var task = _ai.Update(Stable60FpsInterval);
            task.Wait();
        }

        _enemyInRangeSensor.Enemy = new Enemy();

        for (var i = 0; i < 2; i++)
        {
            var task = _ai.Update(Stable60FpsInterval);
            task.Wait();
        }
        
        Assert.Equal(2, ((AttackLeaf)_ai.CurrentAction!).HitsCount);
    }

    [Fact]
    public void TestPatrolThenAttackThenReturnThenPatrol()
    {
        var actionOrder = new[]
        {
            typeof(GoToPointLeaf),
            typeof(AttackLeaf),
            typeof(ReturnToPointLeaf),
            typeof(GoToPointLeaf)
        };

        var idx = 0;
        _ai.ActionSet += action => {
            Assert.IsType(actionOrder[idx++], action);
        };

        _ai.Update(Stable60FpsInterval).Wait();
        
        _enemyInRangeSensor.Enemy = new Enemy();
        _ai.Update(Stable60FpsInterval).Wait();
        
        _enemyInRangeSensor.Enemy = null;
        _aiPositionSensor.AnotherPoint = new Vector2(1000, 1000);
        _ai.Update(Stable60FpsInterval).Wait();
        
        _aiPositionSensor.AnotherPoint = null;
        _ai.Update(Stable60FpsInterval).Wait();

        Assert.Equal(4, idx);
    }
}