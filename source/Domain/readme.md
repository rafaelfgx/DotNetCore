# DotNetCore.Domain

## Entity

```cs
public abstract class Entity
{
    public long Id { get; init; }

    public static bool operator !=(Entity left, Entity right) { }

    public static bool operator ==(Entity left, Entity right) { }

    public override bool Equals(object obj) { }

    public override int GetHashCode() { }
}
```

## Event

```cs
public abstract class Event
{
    public Guid Id { get; } = Guid.NewGuid();

    public DateTime DateTime { get; } = DateTime.UtcNow;
}
```
