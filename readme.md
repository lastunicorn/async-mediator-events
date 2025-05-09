# AsyncMediator Events

AsyncMediator is a clone of MediatR. Here is the GitHub repo:

- https://github.com/jpv001/AsyncMediator

## Problem

The delayed events created using the `IMediator` are not executed at the end of the use case (command or query) execution.

To execute the deferred events, the `IMediator.ExecuteDeferredEvents()` method must be called, but how to ensure its execution after each command or query?

## Solution 1 - Request Bus

Create a wrapper over the `IMediator` implementation which. We can call it `RequestBus`. It will:

- send the requested command/query to the `IMediator` instance;
- execute the delayed events by calling the `IMediator.ExecuteDeferredEvents()` method.

## Solution 2 - Base Classes

Create base classes, one for commands and and one for queries, which will execute the delayed events at the end of the `Handle()` method execution.
