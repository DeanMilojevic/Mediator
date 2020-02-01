# Mediator

This is a simple implementation of the CQRS pattern. It provides the following interfaces to define `Commands`/`Queries`:

```csharp
public interface ICommand
{ }
```

```csharp
public interface ICommandHandler<in T> where T : ICommand
{
    Task Handle(T command);
}
```

```csharp
public interface IQuery<out TResult>
{ }
```

```csharp
public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
{
    Task<TResult> Handle(TQuery query);
}
```

Inside it is also a contract to define the `Dispatcher` that would process dispatched `Commands`/`Queries`:

```csharp
public interface IDispatcher
{
    Task Send(ICommand command);

    Task<T> Send<T>(IQuery<T> query);
}
```

## The example of the `Command` and its `Handler`

```csharp
public class SampleCommand : ICommand
{
    public string Value { get; set; }
}

public class SampleCommandHandler : ICommandHandler<SampleCommand>
{
    private readonly ILogger<SampleCommandHandler> _logger;

    public SampleCommandHandler(ILogger<SampleCommandHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(SampleCommand command)
    {
        _logger.LogInformation(command.Value);

        return Task.CompletedTask;
    }
}
```

Example of usage:

```csharp
private readonly IDispatcher _dispatcher;

...

var command = new SampleCommand
{
    Value = "Value of the Sample Command"
};

await _dispatcher.Send(command);

...
```

## The example of the `Query` and its `Handler`

```csharp
public class SampleQuery : IQuery<SampleQueryResult>
{
    public string Value { get; set; }
}

public class SampleQueryResult
{
    public string Value { get; set; }
}

public class SampleQueryHandler : IQueryHandler<SampleQuery, SampleQueryResult>
{
    private readonly ILogger<SampleQueryHandler> _logger;

    public SampleQueryHandler(ILogger<SampleQueryHandler> logger)
    {
        _logger = logger;
    }

    public async Task<SampleQueryResult> Handle(SampleQuery query)
    {
        _logger.LogInformation(query.Value);

        await Task.Delay(100);

        return new SampleQueryResult { Value = "Result from the Sample Query Handler" };
    }
}
```

Example of usage:

```csharp
private readonly ILogger _logger;
private readonly IDispatcher _dispatcher;

...

var query = new SampleQuery
{
    Value = "Value of the Sample Query"
};

var result = await _dispatcher.Send(query);

_logger.LogInformation(result.Value);

...
```

## Remarks

The examples above don't show the implementation of the `Dispatcher`. One example on how this can be done can be found in the `samples` of the project.

## TODO

Make it packable
