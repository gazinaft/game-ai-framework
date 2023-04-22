# main/godot-ai-framework



## Getting Started

Download links:

SSH clone URL: ssh://git@git.jetbrains.space/punch-dough/main/godot-ai-framework.git

HTTPS clone URL: https://git.jetbrains.space/punch-dough/main/godot-ai-framework.git



These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

## Prerequisites

What things you need to install the software and how to install them.

```
Dotnet 6.0+
```

## Project structure

- `CodeExecution` - build AI from GUI using reflection
- `CodeExecution.Test` - unit tests for CodeExecution
- `CoreEntities` - core entities of AI architecture
- `DecisionMaking` - implementations of decision making algorithms for the framework. Currently only FSM implemented
- `Direction` - facade for all other AI components which is used directly in user's code
- `Framework.Test` - integration test for the framework to test its components working together and a demo of usage

## Deployment

Add additional notes about how to deploy this on a production system.

## Resources

Add links to external resources for this project, such as CI server, bug tracker, etc.
