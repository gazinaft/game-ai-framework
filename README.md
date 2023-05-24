# main/game-ai-framework

## Prerequisites

What things you need to use the framework.

- Dotnet 6.0+
- Game Engine that supports dotnet 6.0+

## Project structure

- `CodeExecution` - build AI from GUI using reflection. GUI is under development in separate repository
- `CodeExecution.Test` - unit tests for CodeExecution
- `CoreEntities` - core entities of AI architecture
- `DecisionMaking` - implementations of decision making algorithms for the framework. Currently only FSM and BT implemented
- `Direction` - facade for all other AI components which is used directly in user's code
- `Framework.BehaviorTree.Test` - integration test for the BT version of framework to test its components working together and a demo of usage
- `Framework.Graph.Test` - integration test for the FSM version of framework to test its components working together and a demo of usage
