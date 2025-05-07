# AsyncMediator Events

AsyncMediator is a clone of MediatR. Here is the GitHub repo:

- https://github.com/jpv001/AsyncMediator

## Problem

The delayed events created using the `IMediator` are not executed at the end of the use case (command or query) execution.

## Solution 1

Create a wrapper over the `IMediator` implementation which will execute all the delayed events after the command/query is finished.

## Solution 2

Create base classes for a command and query which will execute all the delayed events after the command/query is finished.

## Solution 3

Create a "middleware" for the mediator to be executed after the 
