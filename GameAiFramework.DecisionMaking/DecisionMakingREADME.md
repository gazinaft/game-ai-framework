# GameAiFramework.DecisionMaking

## FiniteStateMachine
Has states and transitions which have logic

## BehaviorTree
Consists of tree tasks of different kinds:
- `Selector`: chooses the first available child
- `Sequence`: executes all children in order
- `Decorator`: marks availability of a child tree task, works in pair with Selector
- `Leaf`: represents an action, which an agent will execute

`LeafLogic` and `Decorator` refer to _logic_, rather than structure, like other classes listed higher 