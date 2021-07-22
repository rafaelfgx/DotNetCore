# DotNetCore.Domain

## Entity

```cs
public abstract class Entity<TId> : Base<Entity<TId>>
{
    public TId Id { get; protected set; }

    protected sealed override IEnumerable<object> Equals() { }
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

## ValueObject

```cs
public abstract class ValueObject : Base<ValueObject> { }
```

```cs
public abstract class ValueObject<T> : ValueObject
{
    protected ValueObject(T value) { }

    public T Value { get; }

    protected sealed override IEnumerable<object> Equals() { }
}
```
